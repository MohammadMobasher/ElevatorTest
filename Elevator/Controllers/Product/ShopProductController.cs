using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Utilities;
using Elevator.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Repos;

namespace Elevator.Controllers.Product
{
    public class ShopProductController : Controller
    {
        private readonly ShopProductRepository _shopProductRepository;

        public ShopProductController(ShopProductRepository shopProductRepository)
        {
            _shopProductRepository = shopProductRepository;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = int.Parse(User.Identity.FindFirstValue(ClaimTypes.NameIdentifier));

            var model = await _shopProductRepository.TableNoTracking.Where(a => a.UserId == userId && !a.IsFinaly)
                .ToListAsync();


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
    }
}