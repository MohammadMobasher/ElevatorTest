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
using Microsoft.Extensions.Configuration;
using Core;
using Core.BankCommon.ViewModels;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using DNTPersianUtils.Core;
using DataLayer.SSOT;

namespace Elevator.Controllers
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

            var text = $"{12345},{DateTime.Now.ToPersianDay()}";
            var phoneNumber = "09034537712";

            var smsResult = _smsRestClient.SendByBaseNumber(text, phoneNumber, (int)SmsBaseCodeSSOT.SetOrder);

            return View();
        }

        //public IActionResult TestSms()
        //{
        //    var text = $"{12345},{DateTime.Now.ToPersianDay()}";
        //    var phoneNumber = "09034537712";

        //    var smsResult = _smsRestClient.SendByBaseNumber(text, phoneNumber, (int)SmsBaseCodeSSOT.SetOrder);
        //    return Json(true);
        //}


    }
}
