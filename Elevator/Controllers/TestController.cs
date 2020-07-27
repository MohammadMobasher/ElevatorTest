using Core.BankCommon.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Controllers
{
    public class TestController:Controller
    {
        private readonly ShopOrderRepository _shopOrderRepository;

        public TestController(ShopOrderRepository shopOrderRepository)
        {
            _shopOrderRepository = shopOrderRepository;
        }

        //public async Task<IActionResult> Tariff()
        //{
        //    return Json(await _shopOrderRepository.CalculateTariff(1));
        //}

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
