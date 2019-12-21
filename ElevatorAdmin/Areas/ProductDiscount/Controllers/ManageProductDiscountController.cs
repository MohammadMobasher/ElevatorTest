using System;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.ViewModels.ProductDiscount;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult ProductDiscount(int id)
        {

            return View(id);
        }

        [HttpPost]
        public async Task<IActionResult> ProductDiscount(ProductDiscountInsertViewModel vm, decimal PriceDiscount, decimal PercentDicount)
        {

            vm.Discount = vm.DiscountType == DataLayer.SSOT.ProductDiscountSSOT.Percent ? PercentDicount : PriceDiscount;

            await _productDiscountRepository.MapAddAsync(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            return RedirectToAction("Index", "ManageProduct",new { area ="Product"});
        }
    }
}