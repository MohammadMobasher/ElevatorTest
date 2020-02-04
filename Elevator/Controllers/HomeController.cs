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
using AutoMapper.QueryableExtensions;
using DataLayer.DTO.Products;

namespace Elevator.Controllers
{
    public class HomeController : BaseController
    {

        private readonly SlideShowRepository _slideShowRepository;
        private readonly NewsGroupRepository _newsGroupRepository;
        private readonly ProductRepostitory productRepostitory;
        public HomeController(SlideShowRepository slideShowRepository,
            NewsGroupRepository newsGroupRepository,
            ProductRepostitory productRepostitory)
        {
            _slideShowRepository = slideShowRepository;
            _newsGroupRepository = newsGroupRepository;
            this.productRepostitory = productRepostitory;
        }


        public async Task<IActionResult> Index()
        {

            var specialProduct = await productRepostitory.TableNoTracking
                .Where(a => a.IsSpecialSell && a.IsActive == true)
                .ProjectTo<ProductFullDTO>()
                .OrderByDescending(a => a.CreateDate)
                .Take(12)
                .ToListAsync();


            ViewBag.SpecialProduct = specialProduct;
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error404()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public IActionResult testMobasher()
        {
            
            ViewBag.MQuery = this._newsGroupRepository.ddd();
            return View();
        }


    }
}
