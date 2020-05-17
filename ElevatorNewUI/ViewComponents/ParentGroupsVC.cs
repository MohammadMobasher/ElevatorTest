using DataLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorNewUI.ViewComponents
{
    public class ParentGroupsVC : ViewComponent
    {

        private readonly ProductGroupRepository _productGroupRepository;

        public ParentGroupsVC(ProductGroupRepository productGroupRepository)
        {
            _productGroupRepository = productGroupRepository;
        }



        public IViewComponentResult Invoke()
        {
            return View(_productGroupRepository.GetParents());
        }

    }
}
