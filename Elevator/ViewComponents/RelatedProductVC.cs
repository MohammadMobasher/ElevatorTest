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
    public class RelatedProductVC : ViewComponent
    {
        private readonly ProductRepostitory _productRepostitory;
        private readonly ProductPackageDetailsRepostitory _productPackageDetailsRepostitory;

        public RelatedProductVC(ProductRepostitory productRepostitory,
            ProductPackageDetailsRepostitory productPackageDetailsRepostitory)
        {
            _productRepostitory = productRepostitory;
            _productPackageDetailsRepostitory = productPackageDetailsRepostitory;
        }

        public IViewComponentResult Invoke(int? groupId, int? packageId)
        {
            if (packageId != null)
            {
                var model = _productPackageDetailsRepostitory.TableNoTracking
                    .Include(a => a.Product)
                    .Take(12)
                    .Select(a => a.Product).ToList();

                return View(model);
            }
            else
            {
                var model = _productRepostitory.TableNoTracking.Where(a => groupId != null && a.ProductGroupId == groupId
                && a.IsActive == true).OrderByDescending(a => a.CreateDate).Take(12).ToList();

                return View(model);
            }
        }
    }
}
