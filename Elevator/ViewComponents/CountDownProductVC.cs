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
    public class CountDownProductVC : ViewComponent
    {

        private readonly ProductDiscountRepository _productDiscountRepository;

        public CountDownProductVC(ProductDiscountRepository productDiscountRepository)
        {
            _productDiscountRepository = productDiscountRepository;
        }



        public IViewComponentResult Invoke(int id)
        {
            var model = _productDiscountRepository.TableNoTracking.FirstOrDefault(a => a.ProductId == id);
            return View(model);
        }

    }
}
