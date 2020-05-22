using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorNewUI.ViewComponents
{
    public class ShopProductVC : ViewComponent
    {
        private readonly ShopProductRepository _shopProductRepository;

        public ShopProductVC(ShopProductRepository shopProductRepository)
        {
            _shopProductRepository = shopProductRepository;
        }

        public IViewComponentResult Invoke()
        {
            var userId = User.Identity.GetUserId<int>();

            var model = _shopProductRepository.ShopProductByUserId(userId);

            return View(model);
        }


    }
}
