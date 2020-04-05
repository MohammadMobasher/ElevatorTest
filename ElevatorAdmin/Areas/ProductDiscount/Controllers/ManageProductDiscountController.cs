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
    [Area("ProductDiscount")]
    [ControllerRole("مدیریت تخفیف‌ها")]
    public class ManageProductDiscountController : BaseAdminController
    {
        private readonly ProductDiscountRepository _productDiscountRepository;
        public ManageProductDiscountController(UsersAccessRepository usersAccessRepository
            , ProductDiscountRepository productDiscountRepository) : base(usersAccessRepository)
        {
            _productDiscountRepository = productDiscountRepository;
        }

        public IActionResult Index(int productId)
        {
            return View();
        }

  
        /// <summary>
        /// لیست تخفیف ها روی تمامی محصولات
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ProductDiscountListAll()
        {
            var model = await _productDiscountRepository
                .GetListAsync(a => a.ProductId == null && a.ProductGroupId == null);

            return View(model);
        }


        [ActionRole("تخفیف روی تمامی محصولات")]
        public async Task<IActionResult> DiscountToAll()
        {
            //var dateNow = DateTime.Now;
            //var model = await _productDiscountRepository
            //    .DiscountToAll(a => a.StartDate <= dateNow && a.EndDate >= dateNow && a.ProductGroupId == null && a.ProductId == null
            //    , o => o.OrderBy(a => a.StartDate));

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DiscountToAll(ProductDiscountInsertViewModel vm, decimal PriceDiscount, decimal PercentDicount)
        {

            vm.Discount = vm.DiscountType == DataLayer.SSOT.ProductDiscountSSOT.Percent ? PercentDicount : PriceDiscount;

            await _productDiscountRepository.MapAddAsync(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            return RedirectToAction("Index");
        }


        #region تخفیف روی محصول

        /// <summary>
        /// لیست تخفیف هایی که برای یک محصول به ثبت رسیده است
        /// </summary>
        /// <param name="id">شناسه کالا</param>
        /// <returns></returns>
        public async Task<IActionResult> ProductDiscountList(int id)
        {
            var model = await _productDiscountRepository
                .GetListAsync(a => a.ProductId == id, o => o.OrderByDescending(a => a.Id));

            ViewBag.ProductId = id;

            return View(model);
        }


        [ActionRole("تخفیف روی محصول")]
        public async Task<IActionResult> ProductDiscount(int id)
        {
            if (await _productDiscountRepository.IsProductSubmited(id))
                return RedirectToAction(nameof(ProductDiscountUpdate), new { id });


            return View(id);
        }

        [HttpPost]
        public async Task<IActionResult> ProductDiscount(ProductDiscountInsertViewModel vm, decimal PriceDiscount, decimal PercentDicount)
        {
            if (vm.StartDate >= vm.EndDate)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("تاریخ شروع تخفیف نمی تواند از تاریخ پایان آن کوچکتر باشد"));
                return RedirectToAction("Index", "ManageProduct", new { area = "Product" });
            }

            vm.Discount = vm.DiscountType == DataLayer.SSOT.ProductDiscountSSOT.Percent ? PercentDicount : PriceDiscount;

            await _productDiscountRepository.MapAddAsync(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            return Redirect("/Product/ManageProduct/Index");
        }



        //[ActionRole("ویرایش تخفیف محصول")]
        public async Task<IActionResult> ProductDiscountUpdateManage(int productId)
        {
            var model = await _productDiscountRepository.TableNoTracking.Where(a => a.ProductId == productId)
                .ProjectTo<ProductDiscountDTO>().LastOrDefaultAsync();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ProductDiscountUpdateManage(ProductDiscountUpdateViewModel vm, decimal PriceDiscount, decimal PercentDicount)
        {
            if (vm.StartDate >= vm.EndDate)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("تاریخ شروع تخفیف نمی تواند از تاریخ پایان آن کوچکتر باشد"));
                return RedirectToAction("Index", "ManageProduct", new { area = "Product" });
            }

            vm.Discount = vm.DiscountType == DataLayer.SSOT.ProductDiscountSSOT.Percent ? PercentDicount : PriceDiscount;

            await _productDiscountRepository.UpdateDiscount(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());


            return RedirectToAction(nameof(ProductDiscountList), new { id = vm.ProductId });
        }


        [ActionRole("ویرایش تخفیف محصول")]
        public async Task<IActionResult> ProductDiscountUpdate(int id)
        {
            var model = await _productDiscountRepository.TableNoTracking.Where(a => a.ProductId == id)
                .ProjectTo<ProductDiscountDTO>().LastOrDefaultAsync();

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> ProductDiscountUpdate(ProductDiscountUpdateViewModel vm, decimal PriceDiscount, decimal PercentDicount)
        {
            if (vm.StartDate >= vm.EndDate)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("تاریخ شروع تخفیف نمی تواند از تاریخ پایان آن کوچکتر باشد"));
                return RedirectToAction("Index", "ManageProduct", new { area = "Product" });
            }

            vm.Discount = vm.DiscountType == DataLayer.SSOT.ProductDiscountSSOT.Percent ? PercentDicount : PriceDiscount;

            await _productDiscountRepository.UpdateDiscount(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());


            return Redirect("/Product/ManageProduct/Index");
        }


        #endregion

        //**************************************************************************************************


        #region ProductGroupDiscount

        /// <summary>
        /// لیست تخفیف هایی که روی گروه محصولات به ثبت رسیده است
        /// </summary>
        /// <param name="id">شناسه گروه</param>
        /// <returns></returns>
        [ActionRole("تخفیف روی گروه")]
        public async Task<IActionResult> ProductGroupDiscountList(int id)
        {
            var model = await _productDiscountRepository
                .GetListAsync(a => a.ProductId == null && a.ProductGroupId == id
                , o => o.OrderByDescending(_ => _.Id));

            ViewBag.Id = id;

            return View(model);
        }




        [ActionRole("تخفیف روی گروه")]
        public async Task<IActionResult> ProductGroupDiscount(int id)
        {
            if (await _productDiscountRepository.IsProductGroupSubmited(id))
                return RedirectToAction(nameof(ProductGroupDiscountUpdate), new { id });


           

            return View(id);
        }

        [HttpPost]
        public async Task<IActionResult> ProductGroupDiscount(ProductDiscountInsertViewModel vm, decimal PriceDiscount, decimal PercentDicount)
        {
            if (vm.StartDate >= vm.EndDate)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("تاریخ شروع تخفیف نمی تواند از تاریخ پایان آن کوچکتر باشد"));
                return RedirectToAction("Index", "ManageProductGroup", new { area = "ProductGroup" });
            }

            vm.Discount = vm.DiscountType == DataLayer.SSOT.ProductDiscountSSOT.Percent ? PercentDicount : PriceDiscount;

            await _productDiscountRepository.MapAddAsync(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            return Redirect("/ProductGroup/ManageProductGroup/Index");
        }


        [ActionRole("ویرایش تخفیف روی گروه محصولات")]
        public async Task<IActionResult> ProductGroupDiscountUpdate(int id)
        {
            var model = await _productDiscountRepository.TableNoTracking.Where(a => a.ProductGroupId == id)
                .ProjectTo<ProductDiscountDTO>().FirstOrDefaultAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProductGroupDiscountUpdate(ProductDiscountUpdateViewModel vm, decimal PriceDiscount, decimal PercentDicount)
        {

            if (vm.StartDate >= vm.EndDate)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("تاریخ شروع تخفیف نمی تواند از تاریخ پایان آن کوچکتر باشد"));
                return RedirectToAction("Index", "ManageProductGroup", new { area = "ProductGroup" });
            }


            vm.Discount = vm.DiscountType == ProductDiscountSSOT.Percent ? PercentDicount : PriceDiscount;

            await _productDiscountRepository.UpdateDiscount(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());


            return Redirect("/ProductGroup/ManageProductGroup/Index");
        }


        [ActionRole("ویرایش تخفیف روی گروه محصولات")]
        public async Task<IActionResult> ProductGroupDiscountUpdateManage(int groupId)
        {
            var model = await _productDiscountRepository.TableNoTracking.Where(a => a.ProductGroupId == groupId)
                .ProjectTo<ProductDiscountDTO>().FirstOrDefaultAsync();

          

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProductGroupDiscountUpdateManage(ProductDiscountUpdateViewModel vm, decimal PriceDiscount, decimal PercentDicount)
        {

            if (vm.StartDate >= vm.EndDate)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("تاریخ شروع تخفیف نمی تواند از تاریخ پایان آن کوچکتر باشد"));
                return RedirectToAction("Index", "ManageProductGroup", new { area = "ProductGroup" });
            }


            vm.Discount = vm.DiscountType == ProductDiscountSSOT.Percent ? PercentDicount : PriceDiscount;

            await _productDiscountRepository.UpdateDiscount(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());


            return RedirectToAction(nameof(ProductGroupDiscountList), new { id = vm.ProductGroupId });
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
            TempData.AddResult(await _productDiscountRepository.ArchiveDiscount(model.Id));

            return Redirect(url);

        }
    }
}