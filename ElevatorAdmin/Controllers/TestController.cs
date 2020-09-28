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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
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
            var path = _hostingEnvironment.WebRootPath;
            string[] files = Directory.GetFiles(path + "\\Test");
            foreach (string fileName in files)
            {
                Image src = Image.FromFile($"{fileName}", true);
                string path2 = $"{fileName}";
                string target = $"{path}\\Thump\\";

                //##Device = Desktops
                //##Screen = 1281px to higher resolution desktops

                if (src.Width > 1280)
                {
                    Resize(path2, target + "XL\\", src.Width, src.Height, "XL"); // XL
                    Resize(path2, target + "L\\", (int)(scaled(src.Width, 1280) * (double)src.Width), (int)(scaled(src.Width, 1280) * (double)src.Height), "L"); // L
                    Resize(path2, target + "M\\", (int)(scaled(src.Width, 1024) * (double)src.Width), (int)(scaled(src.Width, 1024) * (double)src.Height), "M"); // M
                    Resize(path2, target + "S\\", (int)(scaled(src.Width, 767) * (double)src.Width), (int)(scaled(src.Width, 767) * (double)src.Height), "S"); // S
                    Resize(path2, target + "XS\\", (int)(scaled(src.Width, 480) * (double)src.Width), (int)(scaled(src.Width, 480) * (double)src.Height), "XS"); // XS
                }
                else if (src.Width <= 1280 && src.Width >= 1025)
                {
                    Resize(path2, target + "XL\\", src.Width, src.Height, "XL"); // XL
                    Resize(path2, target + "L\\", src.Width, src.Height, "L"); // L
                    Resize(path2, target + "M\\", (int)(scaled(src.Width, 1024) * (double)src.Width), (int)(scaled(src.Width, 1024) * (double)src.Height), "M"); // M
                    Resize(path2, target + "S\\", (int)(scaled(src.Width, 767) * (double)src.Width), (int)(scaled(src.Width, 767) * (double)src.Height), "S"); // S
                    Resize(path2, target + "XS\\", (int)(scaled(src.Width, 480) * (double)src.Width), (int)(scaled(src.Width, 480) * (double)src.Height), "XS"); // XS

                }
                else if (src.Width <= 1024 && src.Width >= 768)
                {
                    Resize(path2, target + "XL\\", src.Width, src.Height, "XL"); // XL
                    Resize(path2, target + "L\\", src.Width, src.Height, "L"); // L
                    Resize(path2, target + "M\\", src.Width, src.Height, "M"); // M
                    Resize(path2, target + "S\\", (int)(scaled(src.Width, 767) * (double)src.Width), (int)(scaled(src.Width, 767) * (double)src.Height), "S"); // S
                    Resize(path2, target + "XS\\", (int)(scaled(src.Width, 480) * (double)src.Width), (int)(scaled(src.Width, 480) * (double)src.Height), "XS"); // XS
                }
                else if (src.Width <= 767 && src.Width >= 481)
                {
                    Resize(path2, target + "XL\\", src.Width, src.Height, "XL"); // XL
                    Resize(path2, target + "L\\", src.Width, src.Height, "L"); // L
                    Resize(path2, target + "M\\", src.Width, src.Height, "M"); // M
                    Resize(path2, target + "S\\", src.Width, src.Height, "S"); // S
                    Resize(path2, target + "XS\\", (int)(scaled(src.Width, 480) * (double)src.Width), (int)(scaled(src.Width, 480) * (double)src.Height), "XS"); // XS
                }
                else if (src.Width <= 480 && src.Width >= 320)
                {
                    Resize(path2, target + "XL\\", src.Width, src.Height, "XL"); // XL
                    Resize(path2, target + "L\\", src.Width, src.Height, "L"); // L
                    Resize(path2, target + "M\\", src.Width, src.Height, "M"); // M
                    Resize(path2, target + "S\\", src.Width, src.Height, "S"); // S
                    Resize(path2, target + "XS\\", src.Width, src.Height, "XS"); // XS
                }
            }


            return null;
        }

        private double scaled(int resouce, int target)
        {
            return (((double)target) / ((double)resouce));
        }

        public void Resize(string srcPath, string srcTarget, int width, int height, string name)
        {
            Image image = Image.FromFile(srcPath);
            Bitmap resultImage = Resize(image, width, height);
            string extension = Path.GetExtension(srcPath);
            //resultImage.Save(srcPath.Replace(".png", "_" + width + "x" + height + ".png"));
            //string d = srcTarget + Path.GetFileNameWithoutExtension(srcPath) + Path.GetExtension(srcPath);
            resultImage.Save(srcTarget + Path.GetFileNameWithoutExtension(srcPath) + Path.GetExtension(srcPath));
        }

        //http://stackoverflow.com/questions/11137979/image-resizing-using-c-sharp
        public Bitmap Resize(Image image, int width, int height)
        {

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}