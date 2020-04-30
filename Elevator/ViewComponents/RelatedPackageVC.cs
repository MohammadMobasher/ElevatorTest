using DataLayer.DTO.Products;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elevator.ViewComponents
{
    public class RelatedPackageVC : ViewComponent
    {
        private readonly ProductPackageRepostitory _productPackageRepostitory;

        public RelatedPackageVC(ProductPackageRepostitory productPackageRepostitory)
        {
            _productPackageRepostitory = productPackageRepostitory;
        }

        public IViewComponentResult Invoke()
        {
            var model = _productPackageRepostitory
                .GetList<ProductPackageFullDTO>(a => a.IsActive && !a.IsDeleted && a.IsManager == true);
                

            return View(model.Take(6).ToList());
        }
    }
}
