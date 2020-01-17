using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Elevator.Models;
using Service.Repos;
using Microsoft.AspNetCore.Authorization;

namespace Elevator.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ProductRepostitory productRepostitory;

        public ProductController(ProductRepostitory productRepostitory)
        {
            this.productRepostitory = productRepostitory;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ShowLastProduct()
        {

            return View();
        }
    }
}
