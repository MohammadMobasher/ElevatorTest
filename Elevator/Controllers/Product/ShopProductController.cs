using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core;
using Core.Utilities;
using DNTPersianUtils.Core;
using Elevator.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.Repos;

namespace Elevator.Controllers.Product
{
    public class ShopProductController : BaseController
    {
        private readonly ShopProductRepository _shopProductRepository;
        private readonly ProductRepostitory _productRepostitory;
        private readonly ProductPackageDetailsRepostitory _productPackageDetailsRepostitory;
        private readonly IConfiguration _configuration;
        public ShopProductController(ShopProductRepository shopProductRepository
            , IConfiguration configuration
            , ProductRepostitory productRepostitory
            , ProductPackageDetailsRepostitory productPackageDetailsRepostitory)
        {
            _shopProductRepository = shopProductRepository;
            _configuration = configuration;
            _productRepostitory = productRepostitory;
            _productPackageDetailsRepostitory = productPackageDetailsRepostitory;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = int.Parse(User.Identity.FindFirstValue(ClaimTypes.NameIdentifier));

            var model = await _shopProductRepository
                .TableNoTracking.Where(a => a.UserId == userId && !a.IsFinaly)
                .Include(a => a.Product)
                .Include(a => a.ProductPackage)
                .ToListAsync();

            var test = _configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

            ViewBag.Url = test.SiteConfig.UrlAddress;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCart(int productId, int count )
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var userId = this.GetUserId();

            TempData.AddResult(await _shopProductRepository.AddCart(productId, userId, count));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddPackageCart(int packageId)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var userId = this.GetUserId();

            TempData.AddResult(await _shopProductRepository.AddCart(packageId, userId));

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> RemoveCart(int id)
        {
            TempData.AddResult(await _shopProductRepository.RemoveCart(id));

            return RedirectToAction("Index");
        }

        /// <summary>
        /// محاسبه قیمت نهایی سبد خرید
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CalculateCart()
        {
            var userId = this.GetUserId();

            return Json(await _shopProductRepository.CalculateCartPrice(userId));
        }

        /// <summary>
        /// اضافه نمودن و یا کم کردن تعداد محصول
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CartCount(int id, bool isPlus)
              => Json(await _shopProductRepository.CartCountFunction(id, isPlus));

        /// <summary>
        /// اضافه نمودن و یا کم کردن تعداد محصول
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CartCountValue(int id, int count)
              => Json(await _shopProductRepository.CartCountFunction(id, count));

    }
}