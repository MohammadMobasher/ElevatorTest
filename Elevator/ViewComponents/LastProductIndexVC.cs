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
    public class LastProductIndexVC : ViewComponent
    {

        private readonly ProductRepostitory _productRepostitory;

        public LastProductIndexVC(ProductRepostitory productRepostitory)
        {
            _productRepostitory = productRepostitory;
        }



        public  IViewComponentResult Invoke()
        {
            var LastProduct =  _productRepostitory.TableNoTracking
                .Where(a => a.IsActive == true)
                .ProjectTo<ProductFullDTO>()
                .OrderByDescending(a => a.CreateDate)
                .Take(12)
                .ToList();

            return View(LastProduct);
        }
    }
}
