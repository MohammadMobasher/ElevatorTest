using Core.BankCommon.ViewModels;
using Core.Utilities;
using Dapper;
using DataLayer.DTO.RolesDTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Service.Repos.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Base;

namespace ElevatorAdmin.Controllers
{
    public class TestController : BaseAdminController
    {
        private readonly IDbConnection _connection;
        private readonly IHostingEnvironment _hostingEnvironment;

        public TestController(UsersAccessRepository usersAccessRepository
            , IDbConnection connection
            , IHostingEnvironment hostingEnvironment) : base(usersAccessRepository)
        {
            _connection = connection;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
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

        public static async Task<T> CallApi<T>(string apiUrl, object value)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
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

        public void Test()
        {
            var query = "select Id,UserName from AspNetUsers";

            var execute = _connection.Query<dynamic>(query).ToList();


            var dynamicParam = new DynamicParameters();

            var list = new List<object>();

            foreach (var item in execute)
            {
                dynamicParam.AddDynamicParams(item);

                var title = dynamicParam.ParameterNames.ToList();

                title.ForEach(value => list.Add(dynamicParam.Get<object>(value)));

            }


        }


        public async Task<IActionResult> ResizeTest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResizeTest(IFormFile file)
        {
            var imageResize = new ImageResizer();

            var save = await MFile.Save(file, "Test");

            var path = _hostingEnvironment.WebRootPath;

            imageResize.Resize($"{path}\\{save}", $"{path}\\Thump\\");

            return null;
        }
    }
}