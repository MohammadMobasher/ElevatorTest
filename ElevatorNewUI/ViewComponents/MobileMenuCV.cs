using Microsoft.AspNetCore.Mvc;
using Service.Repos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorNewUI.ViewComponents
{
    public class MobileMenuCV : ViewComponent
    {
        private readonly ProductGroupRepository _productGroupRepository;

        public MobileMenuCV(ProductGroupRepository productGroupRepository)
        {
            _productGroupRepository = productGroupRepository;
        }



        public IViewComponentResult Invoke(int id)
        {
            var model = _productGroupRepository.GetAll();
            return View(model);
        }
    }
}
