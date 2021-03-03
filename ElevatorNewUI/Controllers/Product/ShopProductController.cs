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
using DataLayer.SSOT;
using DataLayer.ViewModels.UsersPayments;
using DNTPersianUtils.Core;
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
        private readonly SmsRestClient _smsRestClient;

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
            , WarehouseProductCheckRepository warehouseProductCheckRepository
            , SmsRestClient smsRestClient)
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
            _smsRestClient = smsRestClient;
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
            if (!User.Identity.IsAuthenticated) return Json("");

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

        public async Task<IActionResult> CreateInvoice(string Title, bool IsInvoice, bool SpecialInvoice)
        {
            var factorId = await _shopOrderRepository.AddFactor(UserId, Title, IsInvoice, SpecialInvoice);

            if (SpecialInvoice)
                return Redirect("/Profile/ListSpecialInvoice");

            if (IsInvoice)
            {
                return Redirect("/Profile/ListInvoice");
            }
            return RedirectToAction("UserAddress", new { id = factorId });
        }

        #endregion




        #region CheckOut

        /// <summary>
        ///  ثبت اطلاعات کاربری از سمت پیش فاکتور
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> UserAddressFromInvoice(int id)
        {
            ViewBag.UserAddress = await _userAddressRepository.GetByConditionAsync(a => a.UserId == UserId);
            ViewBag.OrderId = id;
            ViewBag.UserInfo = await _userRepository.GetByIdAsync(UserId);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserAddressFromInvoice(UserAddress userAddress)
        {
            userAddress.UserId = UserId;
            _userAddressRepository.Submit(userAddress);

            await _shopOrderRepository.SetTariffForFactor(userAddress.ShopOrderId.Value);

            return RedirectToAction(nameof(CheckoutFromInvoice), new { orderId = userAddress.ShopOrderId });
        }


        /// <summary>
        /// ثبت فاکتور و نمایش اطلاعات ان از سمت پیش فاکتور
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<IActionResult> CheckoutFromInvoice(int orderId)
        {
            var listOrders = await _shopProductRepository.GetListAsync(a => a.ShopOrderId == orderId, null, "Product,ProductPackage");

            ViewBag.Unit = await _productUnitRepository.GetListAsync();

            ViewBag.UserInfo = await _userRepository.GetByIdAsync(UserId);

            ViewBag.SumPrice = await _shopProductRepository.CalculateCartPriceFromInvice(orderId);

            ViewBag.Tariff = _shopOrderRepository.CalculateTariffByOrderId(orderId) ?? 0;

            ViewBag.OrderId = orderId;
            ViewBag.UserAddress = await _userAddressRepository.GetByConditionAsync(a => a.UserId == UserId && a.ShopOrderId == orderId);
            return View(listOrders);
        }



        [HttpPost]
        public async Task<IActionResult> SendToBankFromInvoice(int orderId, bool isOnlinePay = true)
        {
            if (!isOnlinePay)
            {
                var createPaymentFactor = await _shopOrderPaymentRepository.CreateOfflinePayment(orderId);

                return RedirectToAction("OrderDetail", "Profile", new { id = orderId });
            }

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



        /// <summary>
        /// جزئیات اطلاعات آدرس کاربر اگر از قبل اطلاعاتی به ثبت رسیده بود نمایش میدهد در غیر اینصورت 
        /// آدرس جدیدی باید ثبت بکند
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> UserAddress(int id)
        {
            ViewBag.UserAddress = await _userAddressRepository.GetByConditionAsync(a => a.UserId == UserId);
            
            //ViewBag.ShopOrderId = id;
            ViewBag.UserInfo = await _userRepository.GetByIdAsync(UserId);
            ViewBag.FactorId = id;
            return View();
        }

        /// <summary>
        /// ثبت اطلاعات آدرس کاربر
        /// </summary>
        /// <param name="userAddress"></param>
        /// <param name="FactorId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UserAddress(UserAddress userAddress, int FactorId)
        {
            userAddress.UserId = UserId;
            userAddress.ShopOrderId = FactorId;
            _userAddressRepository.Submit(userAddress);

            await _shopOrderRepository.SetTariffForFactor(FactorId);

            return RedirectToAction(nameof(Checkout), new { id = FactorId });
        }




        /// <summary>
        /// قبل از وصل شدن به درگاه ابتدا همه ی اطلاعات فاکتور را نمایش می دهیم 
        /// سپس کاربر تصمیم میگیرد که به صورت آنلاین پرداخت کند  یا پرداخت درب منل انجام دهد
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Checkout(int id)
        {
            var listOrders = await _shopProductRepository.GetListAsync(a => a.UserId == UserId && a.ShopOrderId == id
            , null, "Product,ProductPackage");

            ViewBag.Unit = await _productUnitRepository.GetListAsync();

            ViewBag.UserInfo = await _userRepository.GetByIdAsync(UserId);

            ViewBag.SumPrice = await _shopProductRepository.CalculateCartPriceNumber(UserId, id);

            ViewBag.Tariff = _shopOrderRepository.CalculateTariffByOrderId(id) ?? 0;

            ViewBag.FactorId = id;

            ViewBag.UserAddress = await _userAddressRepository.GetByConditionAsync(a => a.UserId == UserId && a.ShopOrderId == id);
            return View(listOrders);
        }

        [HttpPost]
        public async Task<IActionResult> SendToBank(int id, bool isOnlinePay = true)
        {
            var listOrders = await _shopProductRepository.GetListAsync(a => a.UserId == UserId
            && !a.IsFinaly && !a.IsFactorSubmited);

            var orderId = await _shopOrderRepository.UpdatePaymentFactor(id, listOrders);

            // اگر پرداخت درب منزل بود ابتدا برایش یک فاکتور ایجاد میکنیم سپس به صفحه جزئیات فاکتور منتقلش میکنیم
            if (!isOnlinePay)
            {
                var createPayment = await _shopOrderPaymentRepository.CreateOfflinePayment(orderId);


                // ارسال اس ام اس به کاربر جهت ثبت سفارش
                var text = $"{orderId};{DateTime.Now.ToPersianDay()}";
                var phoneNumber = _userRepository.GetByCondition(a => a.Id == UserId).PhoneNumber;

                var smsResult = _smsRestClient.SendByBaseNumber(text, phoneNumber, (int)SmsBaseCodeSSOT.SetOrder);

                // ارسال اس ام اس به مدیریت 
                var ResultTest = $"{DateTime.Now.ToPersianDay()};{orderId}";


                var sms1Result = _smsRestClient.SendByBaseNumber(ResultTest, "09122013443", (int)SmsBaseCodeSSOT.Result);
                var sms2Result = _smsRestClient.SendByBaseNumber(ResultTest, "09351631398", (int)SmsBaseCodeSSOT.Result);
                var sms3Result = _smsRestClient.SendByBaseNumber(ResultTest, "09104996615", (int)SmsBaseCodeSSOT.Result);

                _logRepository.Add(new Log() { Text = $"sms1Result+>{sms1Result.RetStatus}-{sms1Result.Value}-{sms1Result.StrRetStatus}" });
                _logRepository.Add(new Log() { Text = $"sms2Result+>{sms2Result.RetStatus}-{sms2Result.Value}-{sms2Result.StrRetStatus}" });
                _logRepository.Add(new Log() { Text = $"sms3Result+>{sms3Result.RetStatus}-{sms3Result.Value}-{sms3Result.StrRetStatus}" });

               // await _treeRepository.CalculateRateTreeFromAmountAndInsert(result.PaymentAmount, model.UserId);



                return RedirectToAction("OrderDetail", "Profile", new { id = orderId });
            }

            // اگر پرداخت آنلاین بود ابتدا باید چک شود که فاکتور بیشتر از 50 ملیون میباشد یا خیر 
            var countPaymentFactor = await _shopOrderPaymentRepository.CreatePayment(orderId);

            // اگر مبلغ کل بیشتر از 50 ملیون بود به صفحه جزئیات فاکتور منتقل شده 
            if (orderId != 0 && countPaymentFactor > 1)
            {
                return RedirectToAction("OrderDetail", "Profile", new { id = orderId });
            }

            // اگر مبلغ کل کمتر از 50 ملیون بود به درگاه بانک منتقل می شود
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