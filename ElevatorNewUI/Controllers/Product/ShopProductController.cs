using System;
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
using Elevator.Controllers;
using ElevatorNewUI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Service.Repos;
using Service.Repos.BankRepository;
using Service.Repos.Product;
using Service.Repos.User;
using Service.Repos.Warehouses;

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
        private readonly UserAddressRepository _userAddressRepository;
        private readonly UserRepository _userRepository;
        private readonly ShopOrderPaymentRepository _shopOrderPaymentRepository;
        private readonly ProductUnitRepository _productUnitRepository;
        private readonly LogRepository _logRepository;
        private readonly WarehouseProductCheckRepository _warehouseProductCheckRepository;

        public ShopProductController(ShopProductRepository shopProductRepository
            , IConfiguration configuration
            , ProductRepostitory productRepostitory
            , ProductPackageDetailsRepostitory productPackageDetailsRepostitory
            , ShopOrderRepository shopOrderRepository
            , ManageBankService manageBankService
            , UsersPaymentRepository usersPaymentRepository
            , UserAddressRepository userAddressRepository
            , UserRepository userRepository
            , ShopOrderPaymentRepository shopOrderPaymentRepository
            , ProductUnitRepository productUnitRepository
            , LogRepository logRepository
            , WarehouseProductCheckRepository warehouseProductCheckRepository)
        {
            _bankConfig = configuration.GetSection(nameof(BankConfig)).Get<BankConfig>();
            _shopProductRepository = shopProductRepository;
            _configuration = configuration;
            _productRepostitory = productRepostitory;
            _productPackageDetailsRepostitory = productPackageDetailsRepostitory;
            _shopOrderRepository = shopOrderRepository;
            _manageBankService = manageBankService;
            _usersPaymentRepository = usersPaymentRepository;
            _userAddressRepository = userAddressRepository;
            _userRepository = userRepository;
            _shopOrderPaymentRepository = shopOrderPaymentRepository;
            _productUnitRepository = productUnitRepository;
            _logRepository = logRepository;
            _warehouseProductCheckRepository = warehouseProductCheckRepository;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }


            var userId = int.Parse(User.Identity.FindFirstValue(ClaimTypes.NameIdentifier));

            var model = await _shopProductRepository
                .TableNoTracking.Where(a => a.UserId == userId && !a.IsFinaly && !a.IsFactorSubmited)
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

            var result = await _shopProductRepository.AddCart(productId, userId, count);

            if (result.Succeed)
            {
                await _warehouseProductCheckRepository.AddFromShopOrder(new DataLayer.Entities.Warehouse.WarehouseProductCheck
                {
                    Count = count,
                    Date = DateTime.Now,
                    ProductId = productId,
                    TypeSSOt = DataLayer.SSOT.WarehouseTypeSSOT.Out,
                });
            }

            TempData.AddResult(result);


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
            var result = await _shopProductRepository.RemoveCart(id);


            TempData.AddResult(result);


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


        #region پیش‌فاکتور

        public async Task<IActionResult> CreateInvoice(string Title)
        {
            
            await _shopOrderRepository.AddFactor(UserId, Title);
           
            return Redirect("/Profile/ListInvoice");
        }

        #endregion


       

        #region CheckOut

        public async Task<IActionResult> UserAddress()
        {
            ViewBag.UserAddress = await _userAddressRepository.GetByConditionAsync(a => a.UserId == UserId);
            //ViewBag.ShopOrderId = id;
            ViewBag.UserInfo = await _userRepository.GetByIdAsync(UserId);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserAddress(UserAddress userAddress)
        {
            userAddress.UserId = UserId;
            _userAddressRepository.Submit(userAddress);

            return Redirect(nameof(Checkout));
        }

        public async Task<IActionResult> Checkout()
        {
            var listOrders = await _shopProductRepository.GetListAsync(a => a.UserId == UserId &&
                !a.IsFinaly && !a.IsFactorSubmited, null, "Product,ProductPackage");

            ViewBag.Unit = await _productUnitRepository.GetListAsync();

            ViewBag.UserInfo = await _userRepository.GetByIdAsync(UserId);

            ViewBag.SumPrice = await _shopProductRepository.CalculateCartPriceNumber(UserId);

            ViewBag.Tariff = _shopOrderRepository.CalculateTariff(UserId) ?? 0;

            return View(listOrders);
        }

        [HttpPost]
        public async Task<IActionResult> SendToBank()
        {
            var listOrders = await _shopProductRepository.GetListAsync(a => a.UserId == UserId
            && !a.IsFinaly && !a.IsFactorSubmited);

            var orderId = await _shopOrderRepository.CreatePaymentFactor(listOrders.ToList(), UserId);

            var countPaymentFactor = await _shopOrderPaymentRepository.CreatePayment(orderId);

            if (orderId != 0 && countPaymentFactor > 1)
            {
                return RedirectToAction("OrderDetail", "Profile", new { id = orderId });
            }

            else if (orderId != 0 && countPaymentFactor == 1)
            {
                var paymentId = await _shopOrderPaymentRepository.GetByConditionAsync(a => a.ShopOrderId == orderId
                && !a.IsSuccess);

                return await RequestByOrderPayment(paymentId.Id);
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

            var resultAmount = factorInfo.Amount + (factorInfo.TransferProductPrice ?? 0);

            // شماره خرید 
            var OrderId = new Random().Next(1000, int.MaxValue).ToString();

            // رمز گذاری اطلاعات 
            var dataBytes = Encoding.UTF8.GetBytes(string.Format("{0};{1};{2}", _bankConfig.TerminalId, OrderId, resultAmount.CastTomanToRial()));

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
                Amount = resultAmount.CastTomanToRial(),
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

                    return Redirect(string.Format("{0}/Purchase/Index?token={1}", _bankConfig.PurchasePage, res.Result.Token));
                }
                TempData["Result"] = res.Result.Description + " + " + string.Format("{0}/Purchase/Index?token={1}", _bankConfig.PurchasePage, res.Result.Token);

                return RedirectToAction("BankMessage");
            }

            #endregion

            TempData["Result"] = res.Result.Description;

            return RedirectToAction("BankMessage");

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


        /// <summary>
        /// ایجاد درخواست برای اتصال به درگاه بانک
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> RequestByOrderPayment(int id)
        {
            try
            {
                //_logRepository.Add(new Log() { Text = "1" });
                //_logRepository.Add(new Log() { Text = "1.=>"+ _bankConfig.TerminalId });
                //_logRepository.Add(new Log() { Text = "2.=>"+ _bankConfig.PurchasePage });
                // MFile.append("mohammad.txt", "1");
                if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

                var factorInfo = await _shopOrderPaymentRepository
                    .GetByConditionAsync(a => !a.IsSuccess && a.Id == id, isTracked: true);

                #region BankDependency

                if (factorInfo == null)
                {
                    TempData.AddResult(SweetAlertExtenstion.Error("اطلاعات پرداختی با این عنوان یافت نشد"));
                    return RedirectToAction("Index", "ShopProductController");
                }

                var resultAmount = factorInfo.PaymentAmount;


                _logRepository.Add(new Log() { Text = "2=>" + resultAmount.ToString() });
                // شماره خرید 
                var OrderId = new Random().Next(1000, int.MaxValue).ToString();
                //_logRepository.Add(new Log() { Text = "2.1" + _bankConfig.PurchasePage });
                // رمز گذاری اطلاعات 
                var dataBytes = Encoding.UTF8.GetBytes(string.Format("{0};{1};{2}", _bankConfig.TerminalId, OrderId, resultAmount.CastTomanToRial()));
                //_logRepository.Add(new Log() { Text = "2.2" + _bankConfig.PurchasePage });
                var symmetric = SymmetricAlgorithm.Create("TripleDes");
                //_logRepository.Add(new Log() { Text = "2.3" + _bankConfig.PurchasePage });
                symmetric.Mode = CipherMode.ECB;
                //_logRepository.Add(new Log() { Text = "2.4" + _bankConfig.PurchasePage });
                symmetric.Padding = PaddingMode.PKCS7;
                //_logRepository.Add(new Log() { Text = "2.5" + _bankConfig.PurchasePage });

                // رمز گذاری گلید پایانه
                var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(_bankConfig.MerchantKey), new byte[8]);
                //_logRepository.Add(new Log() { Text = "2.6" + _bankConfig.PurchasePage });
                var SignData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));
                //_logRepository.Add(new Log() { Text = "2.7" + _bankConfig.PurchasePage });
                // ادرس بازگشت از درگاه
                var ReturnUrl = string.Format(_bankConfig.SecondReturnUrl);
                //_logRepository.Add(new Log() { Text = "2.8" + _bankConfig.PurchasePage });
                
                // ادرس وب سرویس درگاه
                var ipgUri = string.Format("{0}/api/v0/Request/PaymentRequest", _bankConfig.PurchasePage);
                //_logRepository.Add(new Log() { Text = "2.9" + _bankConfig.PurchasePage });
                #endregion

                #region Informations

                // آماده سازی اطلاعات برای ا
                var data = new
                {
                    _bankConfig.TerminalId,
                    _bankConfig.MerchantId,
                    Amount = resultAmount.CastTomanToRial(),
                    SignData,
                    ReturnUrl = _bankConfig.SecondReturnUrl,
                    LocalDateTime = DateTime.Now,
                    OrderId,
                    //MultiplexingData = request.MultiplexingData
                };


                _logRepository.Add(new Log() { Text = "Data=>" + JsonConvert.SerializeObject(data) });
                //_logRepository.Add(new Log() { Text = "ipgUri=>" + ipgUri });

                #endregion


                #region RequestBuild

                var res = ManageBankService.CallApi<BankResultViewModel>(ipgUri, data);
                res.Wait();
                _logRepository.Add(new Log() { Text = "3Status=>" + res.Status });
                //_logRepository.Add(new Log() { Text = "3ResCode=>" + res.Result?.ResCode });
                #endregion

                #region Request Result

                if (res != null && res.Result != null)
                {
                    _logRepository.Add(new Log() { Text = "3=>" + res.Result.ResCode });
                    if (res.Result.ResCode == "0")
                    {
                        factorInfo.OrderId = OrderId;

                        await _shopProductRepository.UpdateCreateDate(factorInfo.ShopOrderId);
                        await _shopOrderRepository.UpdateCreateDate(factorInfo.ShopOrderId);



                        await _usersPaymentRepository.MapAddAsync(SetValue(res.Result.Token));
                        //await _shopOrderRepository.UpdateAsync(factorInfo);
                        await _shopOrderPaymentRepository.UpdateAsync(factorInfo);

                        return Redirect(string.Format("{0}/Purchase/Index?token={1}", _bankConfig.PurchasePage, res.Result.Token));
                    }
                    //TempData["Result"] = res.Result.Description + " + " + string.Format("{0}/Purchase/Index?token={1}", _bankConfig.PurchasePage, res.Result.Token);
                    //MFile.append("mohammad.txt", "4=>" + res.Result.Description + " + " + string.Format("{0}/Purchase/Index?token={1}", _bankConfig.PurchasePage, res.Result.Token));
                    //_logRepository.Add(new Log() { Text = "4=>" + res.Result.Description });
                    return RedirectToAction("BankMessage");
                }
                #endregion

                TempData["Result"] = res.Result?.Description;

                return RedirectToAction("BankMessage");

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
                        ShopOrderId = factorInfo.ShopOrderId,
                        PaymentId = id,
                    };
                }
                #endregion
            }
            catch (Exception e)
            {
                _logRepository.Add(new Log() { Text = "Exception=>" + e.Message + e.TargetSite });
                throw;
            }
        }

        public IActionResult BankMessage()
        {
            if (TempData["Result"] != null)
            {
                ViewBag.Result = TempData["Result"];
            }
            return View();
        }

        #endregion

        #endregion
    }
}