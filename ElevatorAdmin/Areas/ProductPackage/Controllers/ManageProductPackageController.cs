using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO.ProductFeatures;
using DataLayer.DTO.Products;
using DataLayer.Entities;
using DataLayer.ViewModels;
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
            var model = await _productPackageRepostitory
                .TableNoTracking.Where(a => a.Id == id)
                .ProjectTo<ProductPackageFullDTO>()
                .FirstOrDefaultAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductPackageUpdateViewModel product
            , IFormFile file)
        {
            // ویرایش پکیج
            var productId = await _productPackageRepostitory.UpdateProduct(product, file);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            // بازگشت به لیست محصولات
            return RedirectToAction(nameof(Index));
        }


        #region فعال / غیرفعال

        [ActionRole("فعال / غیر فعال کردن پکیج")]
        public async Task<IActionResult> ChangeState(int id)
        {
            await _productPackageRepostitory.ChangeStateProduct(id);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            return RedirectToAction(nameof(Index));
        }

        [ActionRole("تغییر پکیج عادی به محصول ویژه")]
        public async Task<IActionResult> ChangeSpecialSell(int id)
        {
            await _productPackageRepostitory.ChangeSpecial(id);

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
        [AllowAccess]
        public async Task<IActionResult> PackagePrice(int id)
        {
            var model = await _productPackageDetailsRepostitory.CalculatePrice(id);

            if (model == null) return Json(false);

            return Json(model);
        }

        #endregion

        /// <summary>
        /// ثبت محصول برای پکیج
        /// </summary>
        /// <param name="id">PackageId</param>
        /// <returns></returns>
        public async Task<IActionResult> SubmitProductForPackage(int id)
        {
            ViewBag.PackageId = id;
            return View();
        }


        [AllowAccess]
        public async Task<IActionResult> ProductList(ProductSearchViewModel search, PaginationViewModel page)
        {
            var model = await _productRepostitory.LoadAsyncCount(
                page.CurrentPage,
                page.PageSize,
                search);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = search;

            return PartialView(model.Item2);
        }



        [AllowAccess]
        public async Task<IActionResult> ProductPackageDetail(int id)
        {
            var model = await _productPackageDetailsRepostitory.TableNoTracking
                    .Include(a => a.Product)
                    .Where(a => a.PackageId == id)
                    .ToListAsync();

            ViewBag.PackageId = id;


            return PartialView(model);
        }



        public async Task<IActionResult> SubmitProduct(int productId, int packageId)
        {
            if (await _productPackageDetailsRepostitory.IsExist(packageId, productId))
            {
                return Json(false);
            }

            _productPackageDetailsRepostitory.Add(new ProductPackageDetails()
            {
                PackageId = packageId,
                ProductId = productId
            });

            var model = await _productPackageDetailsRepostitory.TableNoTracking
                .Include(a => a.Product)
                .Where(a => a.PackageId == packageId)
                .ToListAsync();

            ViewBag.PackageId = packageId;
            return PartialView("ProductPackageDetail", model);
        }

        public async Task<IActionResult> RemoveProduct(int productId, int packageId)
        {
            var model = await _productPackageDetailsRepostitory
                .GetByConditionAsync(a => a.PackageId == packageId && a.ProductId == productId);

            if (model == null) return Json(false);

            _productPackageDetailsRepostitory.Delete(model);

            var list = await _productPackageDetailsRepostitory.TableNoTracking
                .Include(a => a.Product)
                .Where(a => a.PackageId == packageId)
                .ToListAsync();

            ViewBag.PackageId = packageId;
            return PartialView("ProductPackageDetail", list);
        }
    }
}