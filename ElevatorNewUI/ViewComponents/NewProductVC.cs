using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorNewUI.ViewComponents
{
    public class NewProductVC : ViewComponent
    {

        private readonly ProductRepostitory _productRepostitory;

        public NewProductVC(ProductRepostitory productRepostitory)
        {
            _productRepostitory = productRepostitory;
        }



        public IViewComponentResult Invoke()
        {
            return View(_productRepostitory.GetList(null, o => o.OrderByDescending(a => a.CreateDate), "", 7));
        }

    }
}
