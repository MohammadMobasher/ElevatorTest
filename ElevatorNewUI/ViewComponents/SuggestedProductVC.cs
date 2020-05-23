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
    public class SuggestedProductVC : ViewComponent
    {
        private readonly ProductRepostitory _productRepostitory;
        public SuggestedProductVC(ProductRepostitory productRepostitory)
        {
            _productRepostitory = productRepostitory;
        }

        public IViewComponentResult Invoke()
        {
            var LastProduct = _productRepostitory.TableNoTracking
                .Where(a => a.IsActive == true && a.IsSpecialSell && !a.IsDeleted)
                .ProjectTo<ProductFullDTO>()
                .OrderByDescending(a => a.CreateDate)
                .Take(12)
                .ToList();


            return View(LastProduct);
        }
    }
}
