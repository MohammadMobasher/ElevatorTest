using DataLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elevator.ViewComponents
{
    public class ProductGroupVC : ViewComponent
    {

        private readonly ProductGroupRepository _productGroupRepository;

        public ProductGroupVC(ProductGroupRepository productGroupRepository)
        {
            _productGroupRepository = productGroupRepository;
        }


        
        public IViewComponentResult Invoke(int id)
        {
            var model = _productGroupRepository.GetParents();
            return View(model);
        }

    }
}
