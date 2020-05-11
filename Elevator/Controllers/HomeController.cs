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

namespace Elevator.Controllers
{
    public class HomeController : BaseController
    {

        private readonly SlideShowRepository _slideShowRepository;
        private readonly NewsGroupRepository _newsGroupRepository;
        private readonly ProductRepostitory productRepostitory;
        private readonly IConfiguration configuration;
        public HomeController(SlideShowRepository slideShowRepository,
            NewsGroupRepository newsGroupRepository,
            ProductRepostitory productRepostitory, IConfiguration configuration)
        {
            _slideShowRepository = slideShowRepository;
            _newsGroupRepository = newsGroupRepository;
            this.productRepostitory = productRepostitory;
            this.configuration = configuration;
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
            return View();
        }


        public IActionResult BankTest()
        {
            var request = new BankPaymentViewModel();

            request.OrderId = new Random().Next(1000, int.MaxValue).ToString();

            var dataBytes = Encoding.UTF8.GetBytes(string.Format("{0};{1};{2}", request.TerminalId, request.OrderId, request.Amount));

            var symmetric = SymmetricAlgorithm.Create("TripleDes");
            symmetric.Mode = CipherMode.ECB;
            symmetric.Padding = PaddingMode.PKCS7;

            var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(request.MerchantKey), new byte[8]);
            request.SignData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

            request.ReturnUrl = string.Format("https://liftbazar.ir/Test/CallBack");

            var ipgUri = string.Format("{0}/api/v0/Request/PaymentRequest", request.PurchasePage);

            var data = new
            {
                request.TerminalId,
                request.MerchantId,
                request.Amount,
                request.SignData,
                request.ReturnUrl,
                LocalDateTime = DateTime.Now,
                request.OrderId,
                //MultiplexingData = request.MultiplexingData
            };

            var res = CallApi<BankResultViewModel>(ipgUri, data);
            res.Wait();

            if (res != null && res.Result != null)
            {
                if (res.Result.ResCode == "0")
                {
                    Response.Redirect(string.Format("{0}/Purchase/Index?token={1}", request.PurchasePage, res.Result.Token));
                }
                ViewBag.Message = res.Result.Description;
                return View();
            }

            return View();
        }

        public IActionResult CallBack(BankVerifyViewModel vm)
        {
            return View(vm);
        }


        public static async Task<T> CallApi<T>(string apiUrl, object value)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                var w = client.PostAsJsonAsync(apiUrl, value);
                w.Wait();
                HttpResponseMessage response = w.Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<T>();
                    result.Wait();
                    return result.Result;
                }
                return default(T);
            }
        }
    }
}
