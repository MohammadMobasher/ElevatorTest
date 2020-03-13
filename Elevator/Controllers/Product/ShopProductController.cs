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
                .Include(a=>a.Product)
                .Include(a=>a.ProductPackage)
                .ToListAsync();

            var test = _configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

            ViewBag.Url = test.SiteConfig.UrlAddress;

            return View(model);
        }


        public async Task<IActionResult> AddCart(int productId)
        {
            var userId = this.GetUserId();

            TempData.AddResult(await _shopProductRepository.AddCart(productId,userId));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddPackageCart(int packageId)
        {
            var userId = this.GetUserId();

            TempData.AddResult(await _shopProductRepository.AddCart(packageId, userId));

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> RemoveCart(int id)
        {
            TempData.AddResult(await _shopProductRepository.RemoveCart(id));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CalculateCart()
        {
            var userId = this.GetUserId();

            var model = _shopProductRepository.TableNoTracking
                .Where(a=>a.UserId == userId && a.IsFinaly == false)
                .Include(a=>a.Product)
                .Include(a=>a.ProductPackage).ToList();

            var sum = default(long);

            foreach (var item in model)
            {
                if (item.ProductId != null)
                {
                    sum += await _productRepostitory.ResultPrice(item.ProductId.Value);
                }
                else if (item.PackageId != null)
                {
                    sum += await _productPackageDetailsRepostitory.ResultPrice(item.PackageId.Value);
                }
            }

            return Json(sum.ToString("n0").ToPersianNumbers());
        }
    }
}