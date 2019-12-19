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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DiscountToAll(ProductDiscountInsertViewModel vm, string returnUrl = "")
        {
            await _productDiscountRepository.MapAddAsync(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            return Ok();
        }
    }
}