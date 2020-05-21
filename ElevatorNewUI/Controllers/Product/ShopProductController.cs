using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.BankCommon.ViewModels;
using Core.Utilities;
using DataLayer.Entities;
using DataLayer.ViewModels.UsersPayments;
using DNTPersianUtils.Core;
using Elevator.Controllers;
using ElevatorNewUI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.Repos;
using Service.Repos.BankRepository;

namespace ElevatorNewUI.Controllers
{
    public class ShopProductController : BaseController
    {
        private readonly ShopProductRepository _shopProductRepository;
        private readonly ShopOrderRepository _shopOrderRepository;
        private readonly ProductRepostitory _productRepostitory;
        private readonly ProductPackageDetailsRepostitory _productPackageDetailsRepostitory;
        private readonly IConfiguration _configuration;
        private readonly ManageBankService _manageBankService;
        private readonly BankConfig _bankConfig;
        private readonly UsersPaymentRepository _usersPaymentRepository;

        public ShopProductController(ShopProductRepository shopProductRepository
            , IConfiguration configuration
            , ProductRepostitory productRepostitory
            , ProductPackageDetailsRepostitory productPackageDetailsRepostitory
            , ShopOrderRepository shopOrderRepository
            , ManageBankService manageBankService
            , UsersPaymentRepository usersPaymentRepository)
        {
            _bankConfig = configuration.GetSection(nameof(BankConfig)).Get<BankConfig>();
            _shopProductRepository = shopProductRepository;
            _configuration = configuration;
            _productRepostitory = productRepostitory;
            _productPackageDetailsRepostitory = productPackageDetailsRepostitory;
            _shopOrderRepository = shopOrderRepository;
            _manageBankService = manageBankService;
            _usersPaymentRepository = usersPaymentRepository;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = int.Parse(User.Identity.FindFirstValue(ClaimTypes.NameIdentifier));

            var model = await _shopProductRepository
                .TableNoTracking.Where(a => a.UserId == userId && !a.IsFinaly)
                .Include(a => a.Product)
                .Include(a => a.ProductPackage)
                .ToListAsync();

            var test = _configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

            ViewBag.Url = test.SiteConfig.UrlAddress;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCart(int productId, int count)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var userId = this.GetUserId();

            TempData.AddResult(await _shopProductRepository.AddCart(productId, userId, count));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddPackageCart(int packageId)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var userId = this.GetUserId();

            TempData.AddResult(await _shopProductRepository.AddCart(packageId, userId));

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddPackageCart(int packageId, int count)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var userId = this.GetUserId();

            TempData.AddResult(await _shopProductRepository.AddPackageCart(packageId, userId, count));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveCart(int id)
        {
            TempData.AddResult(await _shopProductRepository.RemoveCart(id));

            return RedirectToAction("Index");
        }

        /// <summary>
        /// محاسبه قیمت نهایی سبد خرید
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CalculateCart()
        {
            var userId = this.GetUserId();

            return Json(await _shopProductRepository.CalculateCartPrice(userId));
        }

        /// <summary>
        /// اضافه نمودن و یا کم کردن تعداد محصول
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CartCount(int id, bool isPlus, bool isPackage)
              => isPackage ? Json(await _shopProductRepository.CartPackageCountFunction(id, isPlus))
            : Json(await _shopProductRepository.CartCountFunction(id, isPlus));



        /// <summary>
        /// اضافه نمودن و یا کم کردن تعداد محصول
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CartCountValue(int id, int count, bool isPackage)
              => isPackage ? Json(await _shopProductRepository.CartPackageCountFunction(id, count))
            : Json(await _shopProductRepository.CartCountFunction(id, count));


        #region CheckOut

        public async Task<IActionResult> Checkout()
        {
            var listOrders = await _shopProductRepository.GetListAsync(a => a.UserId == UserId
             && !a.IsFinaly, null, "Product,ProductPackage");

            return View(listOrders);
        }

        [HttpPost]
        public async Task<IActionResult> SendToBank()
        {
            var listOrders = await _shopProductRepository.GetListAsync(a => a.UserId == UserId
            && !a.IsFinaly);

            var orderId = await _shopOrderRepository.CreateFactor(listOrders.ToList(), UserId);

            if (orderId != 0)
            {
                return await RequestBuilder(orderId);
            }

            return RedirectToAction("Index");
        }


        #region Bank

        /// <summary>
        /// ایجاد درخواست برای اتصال به درگاه بانک
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> RequestBuilder(int id)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var factorInfo = await _shopOrderRepository.GetByIdAsync(id);

            #region BankDependency

            if (factorInfo == null)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("اطلاعات سبد خریدی با این عنوان یافت نشد"));
                return RedirectToAction("Index", "ShopProductController");
            }

            // شماره خرید 
            var OrderId = new Random().Next(1000, int.MaxValue).ToString();

            // رمز گذاری اطلاعات 
            var dataBytes = Encoding.UTF8.GetBytes(string.Format("{0};{1};{2}", _bankConfig.TerminalId, OrderId, 1000));

            var symmetric = SymmetricAlgorithm.Create("TripleDes");
            symmetric.Mode = CipherMode.ECB;
            symmetric.Padding = PaddingMode.PKCS7;

            // رمز گذاری گلید پایانه
            var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(_bankConfig.MerchantKey), new byte[8]);
            var SignData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

            // ادرس بازگشت از درگاه
            var ReturnUrl = string.Format(_bankConfig.ReturnUrl);

            // ادرس وب سرویس درگاه
            var ipgUri = string.Format("{0}/api/v0/Request/PaymentRequest", _bankConfig.PurchasePage);

            #endregion

            #region Informations

            // آماده سازی اطلاعات برای ا
            var data = new
            {
                _bankConfig.TerminalId,
                _bankConfig.MerchantId,
                Amount = 1000,
                SignData,
                _bankConfig.ReturnUrl,
                LocalDateTime = DateTime.Now,
                OrderId,
                //MultiplexingData = request.MultiplexingData
            };

            #endregion

            #region RequestBuild

            var res = ManageBankService.CallApi<BankResultViewModel>(ipgUri, data);
            res.Wait();

            #endregion

            #region Request Result

            if (res != null && res.Result != null)
            {
                if (res.Result.ResCode == "0")
                {
                    factorInfo.OrderId = OrderId;

                    await _usersPaymentRepository.MapAddAsync(SetValue(res.Result.Token));
                    await _shopOrderRepository.UpdateAsync(factorInfo);

                    Response.Redirect(string.Format("{0}/Purchase/Index?token={1}", _bankConfig.PurchasePage, res.Result.Token));
                }
                ViewBag.Message = res.Result.Description;
                return View();
            }

            #endregion

            return View();

            #region LocalMethods

            AddUserPaymentViewModel SetValue(string token)
            {
                return new AddUserPaymentViewModel()
                {
                    Amount = data.Amount,
                    DateTime = data.LocalDateTime,
                    OrderId = data.OrderId,
                    Token = token,
                    UserId = UserId,
                    ShopOrderId = id
                };
            }

            #endregion
        }

        #endregion

        #endregion
    }
}