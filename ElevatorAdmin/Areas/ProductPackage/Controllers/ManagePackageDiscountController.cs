using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO;
using DataLayer.DTO.ProductDiscounts;
using DataLayer.SSOT;
using DataLayer.ViewModels;
using DataLayer.ViewModels.ProductDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Repos.Product;
using Service.Repos.User;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.ProductDiscount.Controllers
{
    [Area("ProductPackage")]
    [ControllerRole("مدیریت تخفیف‌ پکیج")]
    public class ManagePackageDiscountController : BaseAdminController
    {
        private readonly ProductPackageDiscountRepository _discountRepository;
        public ManagePackageDiscountController(UsersAccessRepository usersAccessRepository
            , ProductPackageDiscountRepository discountRepository) : base(usersAccessRepository)
        {
            _discountRepository = discountRepository;
        }

        public IActionResult Index(int id)
        {
            return View();
        }


        #region تخفیف روی محصول

        /// <summary>
        /// لیست تخفیف هایی که برای یک محصول به ثبت رسیده است
        /// </summary>
        /// <param name="id">شناسه کالا</param>
        /// <returns></returns>
        public async Task<IActionResult> PackageDiscountList(int id)
        {
            var model = await _discountRepository
                .GetListAsync(a => a.PackageId == id, o => o.OrderByDescending(a => a.Id));

            ViewBag.PackageId = id;

            return View(model);
        }


        [ActionRole("تخفیف روی پکیج")]
        public async Task<IActionResult> PackageDiscount(int id)
        {
            if (await _discountRepository.IsPackageSubmited(id))
                return RedirectToAction(nameof(PackageDiscountUpdate), new { id });

            
            return View(id);
        }

        [HttpPost]
        public async Task<IActionResult> PackageDiscount(PackageDiscountInsertViewModel vm, decimal PriceDiscount, decimal PercentDicount)
        {
            if (vm.StartDate >= vm.EndDate)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("تاریخ شروع تخفیف نمی تواند از تاریخ پایان آن کوچکتر باشد"));
                return RedirectToAction("Index", "ManagePackageDiscount", new { area = "ProductPackage" });
            }

            vm.Discount = vm.DiscountType == DataLayer.SSOT.ProductDiscountSSOT.Percent ? PercentDicount : PriceDiscount;

            await _discountRepository.MapAddAsync(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            return Redirect("/ProductPackage/ManageProductPackage/Index");
        }



        //[ActionRole("ویرایش تخفیف محصول")]
        public async Task<IActionResult> PackageDiscountUpdateManage(int PackageId)
        {
            var model = await _discountRepository.TableNoTracking.Where(a => a.PackageId == PackageId)
                .ProjectTo<PackageDiscountDTO>().LastOrDefaultAsync();

            

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> PackageDiscountUpdateManage(PackageDiscountUpdateViewModel vm, decimal PriceDiscount, decimal PercentDicount)
        {
            if (vm.StartDate >= vm.EndDate)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("تاریخ شروع تخفیف نمی تواند از تاریخ پایان آن کوچکتر باشد"));
                return RedirectToAction("Index", "ManageProductPackageController", new { area = "ProductPackage" });
            }

            vm.Discount = vm.DiscountType == DataLayer.SSOT.ProductDiscountSSOT.Percent ? PercentDicount : PriceDiscount;

            var packageId =  await _discountRepository.UpdateDiscount(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());


            return RedirectToAction(nameof(PackageDiscountList), new {  id = packageId });
        }


        [ActionRole("ویرایش تخفیف محصول")]
        public async Task<IActionResult> PackageDiscountUpdate(int id)
        {
            var model = await _discountRepository.TableNoTracking.Where(a => a.PackageId == id)
                .ProjectTo<PackageDiscountDTO>().LastOrDefaultAsync();

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> PackageDiscountUpdate(PackageDiscountUpdateViewModel vm, decimal PriceDiscount, decimal PercentDicount)
        {
            if (vm.StartDate >= vm.EndDate)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("تاریخ شروع تخفیف نمی تواند از تاریخ پایان آن کوچکتر باشد"));
                return RedirectToAction("Index", "ManageProductPackageController", new { area = "ProductPackage" });
            }

            vm.Discount = vm.DiscountType == DataLayer.SSOT.ProductDiscountSSOT.Percent ? PercentDicount : PriceDiscount;

            await _discountRepository.UpdateDiscount(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());


            return Redirect("/ProductPackage/ManageProductPackage/Index");
        }


   
        #endregion

        /// <summary>
        /// آرشیو کردن تخفیف
        /// </summary>
        /// <param name="id"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        [AllowAccess]
        public IActionResult ArchiveDiscount(int Id, string url)
        {
            ViewBag.url = url;
            return View(new DeleteDTO { Id = Id });

        }

        /// <summary>
        /// آرشیو کردن تخفیف
        /// </summary>
        /// <param name="id"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ArchiveDiscount(DeleteViewModel model, string url)
        {
            TempData.AddResult(await _discountRepository.ArchiveDiscount(model.Id));

            return Redirect(url);

        }
    }
}