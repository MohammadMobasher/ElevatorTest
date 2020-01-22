using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Elevator.Models;
using Service.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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


        /// <summary>
        /// نمایش اخرین محصولات ثبت شده
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ShowLastProduct()
        {
            var model = await productRepostitory.TableNoTracking
                .Where(a => a.IsActive == true)
                .OrderByDescending(a => a.CreateDate)
                .Take(7)
                .ToListAsync();

            return PartialView(model);
        }
    }
}
