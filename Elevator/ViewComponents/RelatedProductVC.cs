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
    public class RelatedProductVC : ViewComponent
    {
        private readonly ProductRepostitory _productRepostitory;

        public RelatedProductVC(ProductRepostitory productRepostitory)
        {
            _productRepostitory = productRepostitory;
        }

        public IViewComponentResult Invoke(int groupId)
        {
            var model = _productRepostitory.TableNoTracking.Where(a => a.ProductGroupId == groupId
            && a.IsActive == true).OrderByDescending(a => a.CreateDate).Take(12).ToList();

            return View(model);
        }
    }
}
