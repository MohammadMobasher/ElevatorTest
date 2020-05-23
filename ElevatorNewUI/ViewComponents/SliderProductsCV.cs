using Microsoft.AspNetCore.Mvc;
using Service.Repos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorNewUI.ViewComponents
{
    public class SliderProductsCV : ViewComponent
    {
        private readonly ProductGroupRepository _productGroupRepository;

        public SliderProductsCV(ProductGroupRepository productGroupRepository)
        {
            _productGroupRepository = productGroupRepository;
        }

        public IViewComponentResult Invoke(int id)
        {
            var model = _productGroupRepository.GetAllWith7Product();
            return View(model);
        }
    }
}
