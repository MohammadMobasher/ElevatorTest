using AutoMapper.QueryableExtensions;
using DataLayer.DTO.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Repos;
using Service.Repos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elevator.ViewComponents
{
    public class ProductPackageVC : ViewComponent
    {

        private readonly ProductRepostitory _productRepostitory;
        private readonly ProductGroupRepository _productGroupRepository;
        private readonly ProductPackageDetailsRepostitory _productPackageDetailsRepostitory;

        public ProductPackageVC(ProductRepostitory productRepostitory
            , ProductGroupRepository productGroupRepository
            , ProductPackageDetailsRepostitory productPackageDetailsRepostitory)
        {
            _productRepostitory = productRepostitory;
            _productGroupRepository = productGroupRepository;
            _productPackageDetailsRepostitory = productPackageDetailsRepostitory;
        }



        public IViewComponentResult Invoke(int packageId)
        {
            var model = _productPackageDetailsRepostitory.TableNoTracking
                .Include(a => a.Product)
                .Where(a => a.PackageId == packageId)
                .ToList();

            return View(model);
        }
    }
}
