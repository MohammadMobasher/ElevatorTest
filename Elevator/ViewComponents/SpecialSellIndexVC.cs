using AutoMapper.QueryableExtensions;
using DataLayer.DTO.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elevator.ViewComponents
{
    public class SpecialSellIndexVC : ViewComponent
    {

        private readonly ProductRepostitory _productRepostitory;

        public SpecialSellIndexVC(ProductRepostitory productRepostitory)
        {
            _productRepostitory = productRepostitory;
        }



        public IViewComponentResult Invoke()
        {
            var specialProduct = _productRepostitory.TableNoTracking
                                .Where(a => a.IsSpecialSell && a.IsActive == true && !a.IsDeleted)
                                .ProjectTo<ProductFullDTO>()
                                .OrderByDescending(a => a.CreateDate)
                                .Take(12)
                                .ToList();

            return View(specialProduct);
        }
    }
}
