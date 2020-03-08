using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Utilities;
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
    }
}