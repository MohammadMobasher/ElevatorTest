using AutoMapper.QueryableExtensions;
using DataLayer.DTO.Products;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elevator.ViewComponents
{
    public class LastProductDiscountVC : ViewComponent
    {
        private readonly ProductRepostitory _productRepostitory;

        public LastProductDiscountVC(ProductRepostitory productRepostitory)
        {
            _productRepostitory = productRepostitory;
        }

        public IViewComponentResult Invoke()
        {
            var model = _productRepostitory.TableNoTracking.Where(a => a.IsActive == true
            && a.PriceWithDiscount != a.Price && a.PriceWithDiscount == a.Price).ProjectTo<ProductFullDTO>();

            return View(model.Take(12).OrderByDescending(a=>a.CreateDate).ToList());
        }
    }
}
