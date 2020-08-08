using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ElevatorNewUI.Models;
using Elevator.Controllers;
using Service.Repos;

using DataLayer.DTO.Products;
using AutoMapper.QueryableExtensions;
using Core;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DNTPersianUtils.Core;
using DataLayer.SSOT;

namespace ElevatorNewUI.Controllers
{
    public class HomeController : BaseController
    {

        private readonly SlideShowRepository _slideShowRepository;
        private readonly NewsGroupRepository _newsGroupRepository;
        private readonly ProductRepostitory productRepostitory;
        private readonly IConfiguration configuration;
        private readonly SmsRestClient _smsRestClient;
        public HomeController(SlideShowRepository slideShowRepository,
            NewsGroupRepository newsGroupRepository,
            ProductRepostitory productRepostitory, IConfiguration configuration, SmsRestClient smsRestClient)
        {
            _slideShowRepository = slideShowRepository;
            _newsGroupRepository = newsGroupRepository;
            this.productRepostitory = productRepostitory;
            this.configuration = configuration;
            _smsRestClient = smsRestClient;
        }


        public async Task<IActionResult> Index()
        {

            var specialProduct = await productRepostitory.TableNoTracking
                .Where(a => a.IsSpecialSell && a.IsActive == true)
                .ProjectTo<ProductFullDTO>()
                .OrderByDescending(a => a.CreateDate)
                .Take(12)
                .ToListAsync();

            var test = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

            ViewBag.Url = test.SiteConfig.UrlAddress;
            ViewBag.SpecialProduct = specialProduct;
            return View();
        }

        
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

        public IActionResult TestSms()
        {
            var ResultTest = $"{DateTime.Now.ToPersianDay()};{100000088}";

            var trst= _smsRestClient.SendByBaseNumber(ResultTest, "09034537712", (int)SmsBaseCodeSSOT.Result);
            return Json(trst);
        }
    }
}
