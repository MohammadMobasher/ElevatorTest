using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO.ProductFeatures;
using DataLayer.DTO.Products;
using DataLayer.ViewModels.ProductPackage;
using DataLayer.ViewModels.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Repos;
using Service.Repos.Product;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.Product.Controllers
{
    [Area("ProductPackage")]
    [ControllerRole("مدیریت پکیج ها")]
    public class ManageProductPackageController : BaseAdminController
    {
        private readonly ProductRepostitory _productRepostitory;
        private readonly ProductGroupRepository _productGroupRepository;
        private readonly ProductUnitRepository _productUnitRepository;
        private readonly ProductGroupFeatureRepository _productGroupFeatureRepository;
        private readonly ProductFeatureRepository _productFeatureRepository;
        private readonly ProductGalleryRepository _productGalleryRepository;
        private readonly FeatureRepository _featureRepository;
        private readonly ProductDiscountRepository _productDiscountRepository;
        private readonly ProductPackageRepostitory _productPackageRepostitory;
        private readonly ProductPackageDetailsRepostitory _productPackageDetailsRepostitory;

        public ManageProductPackageController(UsersAccessRepository usersAccessRepository,
            ProductRepostitory productRepostitory,
            ProductGroupRepository productGroupRepository,
            ProductUnitRepository productUnitRepository,
            ProductGroupFeatureRepository productGroupFeatureRepository,
            ProductFeatureRepository productFeatureRepository,
            ProductGalleryRepository productGalleryRepository,
            FeatureRepository featureRepository,
            ProductDiscountRepository productDiscountRepository,
            ProductPackageDetailsRepostitory productPackageDetailsRepostitory,
            ProductPackageRepostitory productPackageRepostitory) : base(usersAccessRepository)
        {
            _productRepostitory = productRepostitory;
            _productGroupRepository = productGroupRepository;
            _productUnitRepository = productUnitRepository;
            _productGroupFeatureRepository = productGroupFeatureRepository;
            _productFeatureRepository = productFeatureRepository;
            _productGalleryRepository = productGalleryRepository;
            _featureRepository = featureRepository;
            _productDiscountRepository = productDiscountRepository;
            _productPackageDetailsRepostitory = productPackageDetailsRepostitory;
            _productPackageRepostitory = productPackageRepostitory;
        }

        [ActionRole("صفحه لیست پکیج ها")]
        public async Task<IActionResult> Index(ProductPackageSearchViewModel searchModel = null)
        {
            var model = await _productPackageRepostitory.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);
        }

        [ActionRole("ثبت پکیج جدید")]
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductPackageInsertViewModel product
            , IFormFile file)
        {
            // ثبت محصول
            var productId = await _productPackageRepostitory.SubmitProduct(product, file);

            // نمایش پیغام
            TempData.AddResult(SweetAlertExtenstion.Ok());

            // بازگشت به لیست محصولات
            return RedirectToAction(nameof(Index));
        }


        [ActionRole("ویرایش محصولات")]
        [HasAccess]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Units = await _productUnitRepository.TableNoTracking.ToListAsync();
            ViewBag.Groups = await _productGroupRepository.TableNoTracking.ToListAsync();

            var model = await _productRepostitory
                .TableNoTracking.Where(a => a.Id == id)
                .ProjectTo<ProductFullDTO>()
                .FirstOrDefaultAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateViewModel product
            , ProductFeatureInsertViewModel vm
            , ProductGalleryViewModel Pics)
        {
            // ثبت محصول
            var productId = await _productRepostitory.UpdateProduct(product, Pics.file);
            vm.ProductId = product.Id;

            if (Pics.galleryImage != null)
            {
                var getAllProductGallery = await _productGalleryRepository.TableNoTracking.Where(a => a.ProductId == product.Id).Select(a => a.Pic).ToListAsync();

                _productGalleryRepository.DeletePic(getAllProductGallery);
                // آپلود گالری
                await _productGalleryRepository.UploadGalley(Pics.galleryImage, productId);
            }

            if (vm.Items != null)
            {
                // ویژگی ها
                await _productFeatureRepository.UpdateFeatureRange(vm);
            }
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
            var groupFeature = await _productGroupFeatureRepository.GetFeaturesByGroupId(id);

            return PartialView(groupFeature);
        }

        /// <summary>
        /// ویژگی های محصولات بر اساس ویژگی های محصول
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAccess]

        public async Task<IActionResult> ProductFeaturesUpdate(int id, int productId)
        {
            var groupFeature = await _productGroupFeatureRepository.GetFeaturesByGroupId(id);

            var productFeature = await _productFeatureRepository.TableNoTracking
                .Where(a => a.ProductId == productId)
                .ProjectTo<ProductFeaturesFullDTO>()
                .ToListAsync();

            ViewBag.ProductFeature = productFeature;

            return PartialView(groupFeature);
        }


        #region ویرایش سریع قیمت
        [ActionRole("ویرایش سریع قیمت")]
        [HasAccess]
        public async Task<IActionResult> FastPriceEdit(int id)
        {
            var model = await _productRepostitory.TableNoTracking
                .ProjectTo<ProductPriceDTO>()
                .FirstOrDefaultAsync(a => a.Id == id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FastPriceEdit(ProductFastPriceEditViewModel vm)
        {
            await _productRepostitory.MapUpdateAsync(vm, vm.Id);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            return RedirectToAction(nameof(Index));
        }

        #endregion

        [AllowAccess]
        public IActionResult mobasher()
        {
            return View();
        }


        #region تغییر ویژگی 

        [ActionRole("تغییر ویژگی‌های کالا")]
        [HasAccess]
        public async Task<IActionResult> SubmitFeature(int id)
        {
            var model = await _productRepostitory.GetFeaturesValuesByProductId(id);

            ViewBag.ProductId = id;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitFeature(ProductFeatureInsertViewModel vm)
        {
            var model = await _productFeatureRepository.UpdateFeatureRange(vm);

            TempData.AddResult(model);

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region فعال / غیرفعال

        [ActionRole("فعال / غیر فعال کردن محصول")]
        public async Task<IActionResult> ChangeState(int id)
        {
            await _productRepostitory.ChangeStateProduct(id);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            return RedirectToAction(nameof(Index));
        }

        [ActionRole("تغییر محصول عادی به محصول ویژه")]
        public async Task<IActionResult> ChangeSpecialSell(int id)
        {
            await _productRepostitory.ChangeSpecial(id);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            return RedirectToAction(nameof(Index));
        }

        #endregion


        #region محاسبه قیمت پکیج

        /// <summary>
        /// محاسبه قیمت پکیج
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> PackagePrice(int id)
        {
            var model =await _productPackageDetailsRepostitory.CalculatePrice(id);

            if (model == null) return Json(false);

            return Json(model);
        }

        #endregion



        public async Task<IActionResult> ProductList(ProductSearchViewModel search)
        {
            var model = await _productRepostitory.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                search);

            this.TotalNumber = model.Item1;


            return PartialView();
        }

    }
}