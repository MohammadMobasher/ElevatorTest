using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO.ProductDiscounts;
using DataLayer.ViewModels.ProductDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Repos.Product;
using Service.Repos.User;
using WebFramework.Base;
namespace ElevatorAdmin.Areas.ProductDiscount.Controllers
{
    [Area("ProductDiscount")]
    public class ManageProductDiscountController : BaseAdminController
    {
        private readonly ProductDiscountRepository _productDiscountRepository;
        public ManageProductDiscountController(UsersAccessRepository usersAccessRepository
            , ProductDiscountRepository productDiscountRepository) : base(usersAccessRepository)
        {
            _productDiscountRepository = productDiscountRepository;
        }

        public IActionResult Index()
        {
            return View();
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

        public async Task<IActionResult> ProductDiscount(int id)
        {
            if (await _productDiscountRepository.IsProductSubmited(id))
                return RedirectToAction(nameof(ProductDiscountUpdate),new { id});


            return View(id);
        }

        [HttpPost]
        public async Task<IActionResult> ProductDiscount(ProductDiscountInsertViewModel vm, decimal PriceDiscount, decimal PercentDicount)
        {

            vm.Discount = vm.DiscountType == DataLayer.SSOT.ProductDiscountSSOT.Percent ? PercentDicount : PriceDiscount;

            await _productDiscountRepository.MapAddAsync(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            return RedirectToAction("Index", "ManageProduct", new { area = "Product" });
        }

        public async Task<IActionResult> ProductDiscountUpdate(int id)
        {
            var model = await _productDiscountRepository.TableNoTracking.Where(a => a.ProductId == id)
                .ProjectTo<ProductDiscountDTO>().FirstOrDefaultAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProductDiscountUpdate(ProductDiscountUpdateViewModel vm, decimal PriceDiscount, decimal PercentDicount)
        {
            vm.Discount = vm.DiscountType == DataLayer.SSOT.ProductDiscountSSOT.Percent ? PercentDicount : PriceDiscount;

            await _productDiscountRepository.UpdateDiscount(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());


            return RedirectToAction("Index", "ManageProduct", new { area = "Product" });
        }



        #region ProductGroupDiscount

        public async Task<IActionResult> ProductGroupDiscount(int id)
        {
            if (await _productDiscountRepository.IsProductGroupSubmited(id))
                return RedirectToAction(nameof(ProductGroupDiscountUpdate), new { id });

            return View(id);
        }

        [HttpPost]
        public async Task<IActionResult> ProductGroupDiscount(ProductDiscountInsertViewModel vm, decimal PriceDiscount, decimal PercentDicount)
        {

            vm.Discount = vm.DiscountType == DataLayer.SSOT.ProductDiscountSSOT.Percent ? PercentDicount : PriceDiscount;

            await _productDiscountRepository.MapAddAsync(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            return RedirectToAction("Index", "ManageProductGroup", new { area = "ProductGroup" });
        }

        public async Task<IActionResult> ProductGroupDiscountUpdate(int id)
        {
            var model = await _productDiscountRepository.TableNoTracking.Where(a => a.ProductGroupId == id)
                .ProjectTo<ProductDiscountDTO>().FirstOrDefaultAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProductGroupDiscountUpdate(ProductDiscountUpdateViewModel vm, decimal PriceDiscount, decimal PercentDicount)
        {
            vm.Discount = vm.DiscountType == DataLayer.SSOT.ProductDiscountSSOT.Percent ? PercentDicount : PriceDiscount;

            await _productDiscountRepository.UpdateDiscount(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());


            return RedirectToAction("Index", "ManageProductGroup", new { area = "ProductGroup" });
        }


        #endregion


        public IActionResult DeleteAll()
        {
            var model = _productDiscountRepository.TableNoTracking.ToList();

            _productDiscountRepository.DeleteRange(model);

            return Json(true);
        }
    }
}