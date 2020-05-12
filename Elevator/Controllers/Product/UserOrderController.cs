using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;

namespace Elevator.Controllers.Product
{
    [Authorize]
    public class UserOrderController : BaseController
    {
        private readonly ShopOrderRepository _shopOrderRepository;

        public UserOrderController(ShopOrderRepository shopOrderRepository)
        {
            _shopOrderRepository = shopOrderRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _shopOrderRepository
                .GetListAsync(a => a.UserId == UserId && a.IsSuccessed);


            return View(model);
        }
    }
}