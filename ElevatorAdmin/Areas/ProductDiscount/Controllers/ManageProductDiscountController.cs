using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO.ProductDiscounts;
using DataLayer.SSOT;
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


        public async Task<IActionResult> ProductDiscountList(int id)
        {
            var model = await _productDiscountRepository
                .TableNoTracking.Where(a => a.ProductId == id)
                .OrderByDescending(a => a.Id)
                .ToListAsync();

            return View(model);
        }

        public async Task<IActionResult> ProductGroupDiscountList(int id)
        {
            var model = await _productDiscountRepository
                .TableNoTracking.Where(a => a.ProductId == null && a.ProductGroupId == id)
                .OrderByDescending(a => a.Id)
                .ToListAsync();

            return View(model);
        }

        public async Task<IActionResult> ProductDiscountListAll(int id)
        {
            var model = await _productDiscountRepository
               .TableNoTracking.Where(a => a.ProductId == null && a.ProductGroupId == null)
                .OrderByDescending(a => a.Id)
                .ToListAsync();

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

            return RedirectToAction("Index", "ManageProduct", new { area = "Product" });
        }


        [ActionRole("ویرایش تخفیف محصول")]
        public async Task<IActionResult> ProductDiscountUpdate(int id)
        {
            var model = await _productDiscountRepository.TableNoTracking.Where(a => a.ProductId == id)
                .ProjectTo<ProductDiscountDTO>().FirstOrDefaultAsync();

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


            return RedirectToAction("Index", "ManageProduct", new { area = "Product" });
        }



        #region ProductGroupDiscount


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

            return RedirectToAction("Index", "ManageProductGroup", new { area = "ProductGroup" });
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


            return RedirectToAction("Index", "ManageProductGroup", new { area = "ProductGroup" });
        }


        #endregion

        [AllowAccess]
        public async Task<IActionResult> CalculateDiscount(decimal price, int? productId, int? groupId)
        {

            var discount = await _productDiscountRepository.CalculatePrice(productId, groupId);

            if (discount == null) return Json(price.ToString("n0"));


            if (discount.DiscountType == ProductDiscountSSOT.Percent)
            {
                var discountVal = (price * discount.Discount) / 100;
                return Json((price - discountVal).ToString("n0"));
            }

            return Json((price - discount.Discount).ToString("n0"));
        }


        /// <summary>
        /// آرشیو کردن تخفیف
        /// </summary>
        /// <param name="id"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<IActionResult> ArchiveDiscount(int id, string url)
        {
            TempData.AddResult(await _productDiscountRepository.ArchiveDiscount(id));

            return Redirect(url);

        }
    }
}