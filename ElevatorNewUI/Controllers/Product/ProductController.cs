using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ElevatorNewUI.Models;
using Elevator.Controllers;
using Service.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Service.Repos.Product;
using DataLayer.SSOT;
using DNTPersianUtils.Core;
using DataLayer.ViewModels.Products;
using Core.Utilities;
using Microsoft.Extensions.Options;
using Core;
using Microsoft.Extensions.Configuration;
using DataLayer.Entities;
using DataLayer.ViewModels.Feature;
using AutoMapper.QueryableExtensions;
using DataLayer.DTO.Products;
using DataLayer.DTO;

namespace ElevatorNewUI.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ProductRepostitory _productRepository;
        private readonly ProductDiscountRepository _productDiscountRepository;
        private readonly ProductGalleryRepository _productGalleryRepository;
        private readonly ProductPackageRepostitory _productPackageRepostitory;
        private readonly ProductGroupRepository _productGroupRepository;
        private readonly ProductFeatureRepository _productFeatureRepository;
        private readonly FeatureItemRepository _featureItemRepository;
        private readonly ProductGroupFeatureRepository _productGroupFeatureRepository;
        private readonly ProductPackageGroupRepository _productPackageGroupRepository;
        private readonly ProductPackageDetailsRepostitory _productPackageDetailsRepostitory;
        private readonly ProductUnitRepository _productUnitRepository;

        public IConfiguration configuration { get; }
        public ProductController(ProductRepostitory productRepostitory,
            ProductDiscountRepository productDiscountRepository,
            IConfiguration Configuration,
            ProductGalleryRepository productGalleryRepository,
            ProductPackageRepostitory productPackageRepostitory,
            ProductGroupRepository productGroupRepository,
            ProductFeatureRepository productFeatureRepository,
            FeatureItemRepository featureItemRepository,
            ProductGroupFeatureRepository productGroupFeatureRepository,
            ProductPackageGroupRepository productPackageGroupRepository,
            ProductPackageDetailsRepostitory productPackageDetailsRepostitory,
            ProductUnitRepository productUnitRepository)
        {
            _productRepository = productRepostitory;
            _productDiscountRepository = productDiscountRepository;
            configuration = Configuration;
            _productGalleryRepository = productGalleryRepository;
            _productPackageRepostitory = productPackageRepostitory;
            _productGroupRepository = productGroupRepository;
            _productFeatureRepository = productFeatureRepository;
            _featureItemRepository = featureItemRepository;
            _productGroupFeatureRepository = productGroupFeatureRepository;
            _productPackageGroupRepository = productPackageGroupRepository;
            _productPackageDetailsRepostitory = productPackageDetailsRepostitory;
            _productUnitRepository = productUnitRepository;
        }

        /// <summary>
        /// لیست تمامی محصولات
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(ProductSearchListViewModel vm)
        {
            var test = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

            var model = await _productRepository.GetProducts(vm, this.CurrentPage, this.PageSize);

            ViewBag.Category = await _productGroupRepository.GetParentsAsync();
            ViewBag.Url = test.SiteConfig.UrlAddress;
            ViewBag.Search = vm;
            ViewBag.MaxPrice = model != null && model.Item2.Count > 0 ? model.Item2.Max(a => a.Price) : 1000000;
            ViewBag.Count = model.Item2.Count().ToPersianNumbers();

            this.TotalNumber = model.Item1;

            return View(model.Item2);
        }


        /// <summary>
        /// لیست تمامی محصولات
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public async Task<IActionResult> ProductDiscountIndex(ProductSearchListViewModel vm)
        {
            var test = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

            var model = await _productRepository.GetProductsDiscount(vm, this.CurrentPage, this.PageSize);

            ViewBag.Category = await _productGroupRepository.GetParentsAsync();
            ViewBag.Url = test.SiteConfig.UrlAddress;
            ViewBag.Search = vm;
            ViewBag.MaxPrice = model != null && model.Item2.Count > 0 ? model.Item2.Max(a => a.Price) : 1000000;
            ViewBag.Count = model.Item2.Count().ToPersianNumbers();

            this.TotalNumber = model.Item1;

            return View(model.Item2);
        }


        /// <summary>
        /// جزئیات محصول
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ProductDetail(int id)
        {
            var model = await _productRepository.GetProductDetail(id);

            if (model == null)
                return NotFound();

            ViewBag.Gallery = await _productGalleryRepository.GetListGalleryByProductId(id);


            List<ProductFeature> features = await _productFeatureRepository.GetFeaturesByProductId(id);

            ViewBag.Features = features;

            List<int> featuresWithSSOTType = _productFeatureRepository.FeaturesWithSSOTType(features);

            if (featuresWithSSOTType != null && featuresWithSSOTType.Count > 0)
            {
                ViewBag.FeatureSSOTValue = await _featureItemRepository.TableNoTracking.Where(x =>
                featuresWithSSOTType.Contains(x.FeatureId)).ToListAsync();
            }

            ViewBag.Unit = await _productUnitRepository.GetListAsync();

            var test = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

            ViewBag.Url = test.SiteConfig.UrlAddress;

            ViewBag.RelatedProducts = _productRepository.GetList(a => a.ProductGroupId == model.ProductGroupId 
            && a.Id != model.Id && !a.IsDeleted,o=>o.OrderByDescending(a=>a.CreateDate)).Take(12).ToList();

            return View(model);
        }

        public async Task<IActionResult> PackageDetail(int id)
        {

            var package = await _productPackageRepostitory.TableNoTracking
                .Include(a => a.ProductPackageDetails)
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

            if (package == null)
                return NotFound();

            ViewBag.Groups = await _productPackageGroupRepository.getItemsByPackageId(id);
            ViewBag.Products = await _productPackageDetailsRepostitory.GetProductByPackageId(id);

            

            var productIds = package.ProductPackageDetails.Select(a => a.ProductId).ToList();


            var test = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

            ViewBag.Url = test.SiteConfig.UrlAddress;
            ViewBag.Gallery = await _productRepository.TableNoTracking.Where(a => productIds.Contains(a.Id)).ToListAsync();

            return View(package);
        }


        /// <summary>
        /// محاسبه تخفیف محصول
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CalculateDiscount(int id)
        {
            var productDiscount = await _productDiscountRepository.GetByConditionAsync(a => a.ProductId == id);
            if (productDiscount == null) return Json(false);

            if (DateTime.Now > productDiscount.StartDate && DateTime.Now < productDiscount.EndDate)
            {

                var product = await _productRepository.GetByIdAsync(id);

                var calculate = productDiscount.DiscountType == ProductDiscountSSOT.Percent ?
                    (product.Price - (product.Price * productDiscount.Discount) / 100)
                    : (product.Price - productDiscount.Discount);

                return Json(new Tuple<string, string, int, DateTime>(calculate.ToString("n0").ToPersianNumbers(), productDiscount.Discount.ToString("n0").ToPersianNumbers(), (int)productDiscount.DiscountType, productDiscount.EndDate));
            }

            return Json(false);
        }

        /// <summary>
        /// افزودن تعداد بازدید کننده
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task AddVisit(int id)
        {
            await _productRepository.VisitPlus(id);
        }

        /// <summary>
        /// اضافه کردن لایک
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> AddLike(int id)
        {
            var model = await _productRepository.AddLike(id);

            return Json(model);
        }

        /// <summary>
        /// اضافه کردین دیس لایک
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<IActionResult> AddDisLike(int id)
        {
            var model = await _productRepository.AddDisLike(id);

            return Json(model);
        }


        public async Task<IActionResult> ProductGroup(int id, bool newSearch, string titleSearch, List<int> selectedSubGroup, string featureValue)
        {
            if (newSearch)
                this.CurrentPage = 1;

            #region تبدیل مقادیر جستجو
            // در این قسمت ایتم هایی که کاربر انتخاب کرده است از روی ویژگی ها به 
            // فرمتی که تابع بتواند آنها را روی کوئری اجرا کند تبدیل میکند
            List<FeatureSearchableViewModel> featureSearchableVM = new List<FeatureSearchableViewModel>();
            if (!string.IsNullOrEmpty(featureValue))
            {
                foreach (var item in featureValue.Split(",", StringSplitOptions.RemoveEmptyEntries))
                {
                    featureSearchableVM.Add(new FeatureSearchableViewModel
                    {
                        FeaureId = Convert.ToInt32(item.Split("_")[0]),
                        FeatureValue = item.Split("_")[1]
                    });
                }
            }
            #endregion


            var test = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
            var result = await _productGroupRepository.GetByParentId(id, true);


            var reuslt = await _productGroupFeatureRepository.SearchableFeatureByGroupId(id);
            ViewBag.Url = test.SiteConfig.UrlAddress;            
            ViewBag.Group = result.SingleOrDefault(x => x.Id == id);
            ViewBag.UndeGroups = result.Where(x => x.Id != id).ToList();
            ViewBag.titleSearch = titleSearch;
            ViewBag.searchableFeature = await _productGroupFeatureRepository.SearchableFeatureByGroupId(id);
            ViewBag.featureSearchableValue = featureSearchableVM;



            var model = await _productRepository.GetProductQuery(id, titleSearch, selectedSubGroup, featureSearchableVM, this.CurrentPage,
                this.PageSize);

            this.TotalNumber = model.Item1;

            return View(model.Item2);
        }

        public async Task<IActionResult> CalculatePrice(int productId)
        {


            var product = await _productRepository.GetByIdAsync(productId);
            var productDiscount = await _productDiscountRepository.GetByConditionAsync(a => a.ProductId == productId);
            if (productDiscount == null) return Json(product.Price.ToString("n0").ToPersianNumbers());

            if (DateTime.Now > productDiscount.StartDate && DateTime.Now < productDiscount.EndDate)
            {


                var calculate = productDiscount.DiscountType == ProductDiscountSSOT.Percent ?
                    (product.Price - (product.Price * productDiscount.Discount) / 100)
                    : (product.Price - productDiscount.Discount);

                return Json(calculate.ToString("n0").ToPersianNumbers());
            }

            return Json(product.Price.ToString("n0").ToPersianNumbers());
        }

        public IActionResult GetByGroupId(int id)
        {
            var model = _productGroupRepository.GetProductByGroupId(id);

            return PartialView(model);
        }
    }
}
