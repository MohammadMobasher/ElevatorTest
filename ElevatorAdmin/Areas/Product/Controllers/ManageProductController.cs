using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO.Products;
using DataLayer.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Repos;
using Service.Repos.Product;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.Product.Controllers
{
    [Area("Product")]
    [ControllerRole("مدیریت کالا‌ها")]
    public class ManageProductController : BaseAdminController
    {
        private readonly ProductRepostitory _productRepostitory;
        private readonly ProductGroupRepository _productGroupRepository;
        private readonly ProductUnitRepository _productUnitRepository;
        private readonly ProductGroupFeatureRepository _productGroupFeatureRepository;
        private readonly ProductFeatureRepository _productFeatureRepository;
        private readonly ProductGalleryRepository _productGalleryRepository;
        private readonly FeatureRepository _featureRepository;

        public ManageProductController(UsersAccessRepository usersAccessRepository,
            ProductRepostitory productRepostitory,
            ProductGroupRepository productGroupRepository,
            ProductUnitRepository productUnitRepository,
            ProductGroupFeatureRepository productGroupFeatureRepository,
            ProductFeatureRepository productFeatureRepository,
            ProductGalleryRepository productGalleryRepository,
            FeatureRepository featureRepository) : base(usersAccessRepository)
        {
            _productRepostitory = productRepostitory;
            _productGroupRepository = productGroupRepository;
            _productUnitRepository = productUnitRepository;
            _productGroupFeatureRepository = productGroupFeatureRepository;
            _productFeatureRepository = productFeatureRepository;
            _productGalleryRepository = productGalleryRepository;
            _featureRepository = featureRepository;
        }

        [ActionRole("صفحه لیست کالاها")]
        //[HasAccess]
        public IActionResult Index()
        {
            var model = _productRepostitory.TableNoTracking.ProjectTo<ProductFullDTO>().ToList();
            return View(model);
        }

        [ActionRole("ثبت کالای جدید")]
        public async Task<IActionResult> Create(int? id)
        {
            ViewBag.Units = await _productUnitRepository.TableNoTracking.ToListAsync();

            if (id != null) return View(id);
            ViewBag.Groups = await _productGroupRepository.TableNoTracking.ToListAsync();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductInsertViewModel product
            , ProductFeatureInsertViewModel vm
            , ProductGalleryViewModel Pics)
        {
           
            // ثبت محصول
            var productId = await _productRepostitory.SubmitProduct(product,Pics.file);

            vm.ProductId = productId;

            // آپلود گالری
            await _productGalleryRepository.UploadGalley(Pics.gallery, productId);

            // ویژگی ها
            await _productFeatureRepository.AddFeatureRange(vm);

            // نمایش پیغام
            TempData.AddResult(SweetAlertExtenstion.Ok());

            // بازگشت به لیست محصولات
            return RedirectToAction(nameof(Index));
        }



        /// <summary>
        /// ویژگی های محصولات بر اساس ویژگی های محصول
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAccess]

        public async Task<IActionResult> ProductFeatures(int id)
        {
            var groupFeature = await _productGroupFeatureRepository.GetAllProductGroupFeature(id);

            var productFeatures = await _productFeatureRepository.GetAllProductFeatureByProductId(id);

            var feautreitem = await _featureRepository.GetFeaturesByListFeatureId(groupFeature.Select(a => a.FeatureId).ToList());

            ViewBag.ProductId = id;
            ViewBag.ProductFeatures = productFeatures;
          
            return PartialView(feautreitem);
        }



        #region ثبت ویژگی (خدابیامرز) منقضی 

        [ActionRole("ثبت ویژگی‌های کالا")]
        [HasAccess]
        public async Task<IActionResult> SubmitFeature(int id)
        {
            var groupId = await _productRepostitory.GetProductGroupIdbyProductId(id);

            if (groupId == null)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("کالایی با این شناسه یافت نشد"));

                return RedirectToAction(nameof(Index));
            }

            var features = await _productGroupFeatureRepository.GetAllProductGroupFeature(groupId.Value);

            var productFeatures = await _productFeatureRepository.GetAllProductFeatureByProductId(id);

            ViewBag.ProductId = id;
            ViewBag.ProductFeatures = productFeatures;

            return View(features);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitFeature(ProductFeatureInsertViewModel vm)
        {
            var model = await _productFeatureRepository.AddFeatureRange(vm);

            TempData.AddResult(model);

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}