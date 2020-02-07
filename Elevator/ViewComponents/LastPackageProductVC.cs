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
    public class LastPackageProductVC : ViewComponent
    {
        private readonly ProductPackageRepostitory _productPackageRepostitory;

        public LastPackageProductVC(ProductPackageRepostitory productPackageRepostitory)
        {
            _productPackageRepostitory = productPackageRepostitory;
        }

        public IViewComponentResult Invoke()
        {
            var lastPackage = _productPackageRepostitory.TableNoTracking
                .Where(a => a.IsActive == true)
                .ProjectTo<ProductPackageFullDTO>()
                .OrderByDescending(a => a.CreateDate)
                .Take(12)
                .ToList();

            return View(lastPackage);
        }
    }
}
