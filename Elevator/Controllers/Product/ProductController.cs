using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Elevator.Models;
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

namespace Elevator.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ProductRepostitory _productRepository;
        private readonly ProductDiscountRepository _productDiscountRepository;
        private readonly ProductGalleryRepository _productGalleryRepository;
        private readonly ProductPackageRepostitory _productPackageRepostitory;
        private readonly ProductGroupRepository _productGroupRepository;
        private readonly ProductFeatureRepository _productFeatureRepository;

        public IConfiguration configuration { get; }
        public ProductController(ProductRepostitory productRepostitory,
            ProductDiscountRepository productDiscountRepository,
            IConfiguration Configuration,
            ProductGalleryRepository productGalleryRepository,
            ProductPackageRepostitory productPackageRepostitory,
            ProductGroupRepository productGroupRepository,
            ProductFeatureRepository productFeatureRepository)
        {
            _productRepository = productRepostitory;
            _productDiscountRepository = productDiscountRepository;
            configuration = Configuration;
            _productGalleryRepository = productGalleryRepository;
            _productPackageRepostitory = productPackageRepostitory;
            _productGroupRepository = productGroupRepository;
            _productFeatureRepository = productFeatureRepository;
        }

        /// <summary>
        /// لیست تمامی محصولات
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(ProductSearchListViewModel vm)
        {
            var test = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

            var model = _productRepository.TableNoTracking
                .Include(a => a.ProductGroup)
                .Where(a => a.IsActive == true);

            var result = await model
                .WhereIf(string.IsNullOrEmpty(vm.Title), a => a.Title.Contains(vm.Title) 
                || a.ShortDescription.Contains(vm.Title)
                || a.Text.Contains(vm.Title)
                || a.Tags.Contains(vm.Title))
                .WhereIf(vm.Group != null, a => a.ProductGroupId.Equals(vm.Group.Value))
                .WhereIf(vm.MaxPrice != null && vm.MinPrice != null, a => a.Price >= long.Parse(vm.MinPrice) && a.Price <= long.Parse(vm.MaxPrice))
                .ToListAsync();

            ViewBag.Category = await _productGroupRepository.GetParentsAsync();
            ViewBag.Url = test.SiteConfig.UrlAddress;
            ViewBag.Search = vm;
            ViewBag.MaxPrice = result != null && result.Count > 0 ? result.Max(a => a.Price) : 1000000;
            ViewBag.Count = result.Count().ToPersianNumbers();

            return View(result);
        }

        /// <summary>
        /// جزئیات محصول
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ProductDetail(int id)
        {
            var model = await _productRepository.TableNoTracking
                .Include(a => a.ProductGroup)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (model == null)
                return NotFound();

            ViewBag.Gallery = await _productGalleryRepository.TableNoTracking.Where(a => a.ProductId == id).ToListAsync();

            ViewBag.Discount = await _productDiscountRepository.TableNoTracking.Where(a => a.ProductId == id)
                .FirstOrDefaultAsync();

            ViewBag.Features = await _productFeatureRepository.TableNoTracking.Include(a => a.Feature)
                .Where(a => a.ProductId == id).ToListAsync();


            var test = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

            ViewBag.Url = test.SiteConfig.UrlAddress;

            return View(model);
        }

        public async Task<IActionResult> PackageDetail(int id)
        {
            var package = await _productPackageRepostitory.TableNoTracking
                .Include(a => a.ProductPackageDetails)
                .FirstOrDefaultAsync(a => a.Id == id);

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


        public async Task<IActionResult> ProductGroup(int id)
        {
            ViewBag.Group = await _productGroupRepository.GetByIdAsync(id);
            var model = await _productRepository.GetProductQuery(id);
            return View(model);
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


    }
}
