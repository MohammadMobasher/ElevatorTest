using AutoMapper.QueryableExtensions;
using DataLayer.DTO.Products;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorNewUI.ViewComponents
{
    public class SpecialProductVC : ViewComponent
    {

        private readonly ProductRepostitory _productRepostitory;

        public SpecialProductVC(ProductRepostitory productRepostitory)
        {
            _productRepostitory = productRepostitory;
        }



        public IViewComponentResult Invoke()
        {
            return View(
                _productRepostitory.TableNoTracking
                .Where(a => a.IsActive == true &&
                a.IsSpecialSell && 
                //a.Price != a.PriceWithDiscount &&
                !a.IsDeleted)
                .ProjectTo<ProductFullDTO>()
                .OrderByDescending(a => a.CreateDate)
                .Take(6)
                .ToList());
        }

    }
}
