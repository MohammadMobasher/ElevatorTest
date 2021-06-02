using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ElevatorNewUI.Models;
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
using Service.Repos.BankRepository;
using DataLayer.Entities.Bank;
using DataLayer.ViewModels.UsersPayments;
using Core.Utilities;
using System.Security.Claims;
using Elevator.Controllers;
using Service.Repos.User;
using DataLayer.SSOT;
using DNTPersianUtils.Core;
using Service.Repos.Product;
using DataLayer.ViewModels.ShopOrderStatus;
using Service.Repos.TreeInfo;
using DataLayer.Entities;

namespace ElevatorNewUI.Controllers
{
    [Authorize]
    public class ManageBankService : BaseController
    {
        private readonly BankConfig _bankConfig;
        private readonly UsersPaymentRepository _usersPaymentRepository;
        private readonly ShopProductRepository _shopProductRepository;
        private readonly ShopOrderRepository _shopOrderRepository;
        private readonly SmsRestClient _smsRestClient;
        private readonly UserRepository _userRepository;
        private readonly ShopOrderStatusRepository _shopOrderStatusRepository;
        private readonly ShopOrderPaymentRepository _shopOrderPaymentRepository;
        private readonly TreeRepository _treeRepository;
        private readonly LogRepository _logRepository;

        public ManageBankService(IConfiguration configuration
            , UsersPaymentRepository usersPaymentRepository
            , ShopProductRepository shopProductRepository
            , ShopOrderRepository shopOrderRepository
            , SmsRestClient smsRestClient
            , UserRepository userRepository
            , ShopOrderStatusRepository shopOrderStatusRepository
            , ShopOrderPaymentRepository shopOrderPaymentRepository
            , TreeRepository treeRepository
            , LogRepository logRepository)
        {
            _bankConfig = configuration.GetSection(nameof(BankConfig)).Get<BankConfig>();
            _usersPaymentRepository = usersPaymentRepository;
            _shopProductRepository = shopProductRepository;
            _shopOrderRepository = shopOrderRepository;
            _smsRestClient = smsRestClient;
            _userRepository = userRepository;
            _shopOrderStatusRepository = shopOrderStatusRepository;
            _shopOrderPaymentRepository = shopOrderPaymentRepository;
            _treeRepository = treeRepository;
            _logRepository = logRepository;
        }

        ///// <summary>
        ///// ایجاد درخواست برای اتصال به درگاه بانک
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public async Task<IActionResult> RequestBuilder(int id,int userId)
        //{
        //    //if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

        //    var cartInfo = await _shopOrderRepository.GetByIdAsync(id);

        //    if(cartInfo == null)
        //    {
        //        TempData.AddResult(SweetAlertExtenstion.Error("اطلاعات سبد خریدی با این عنوان یافت نشد"));
        //        return RedirectToAction("Index", "ShopProductController");
        //    }

        //    var OrderId = new Random().Next(1000, int.MaxValue).ToString();

        //    var dataBytes = Encoding.UTF8.GetBytes(string.Format("{0};{1};{2}", _bankConfig.TerminalId, OrderId, 1000));

        //    var symmetric = SymmetricAlgorithm.Create("TripleDes");
        //    symmetric.Mode = CipherMode.ECB;
        //    symmetric.Padding = PaddingMode.PKCS7;

        //    var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(_bankConfig.MerchantKey), new byte[8]);
        //    var SignData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

        //    var ReturnUrl = string.Format(_bankConfig.ReturnUrl);

        //    var ipgUri = string.Format("{0}/api/v0/Request/PaymentRequest", _bankConfig.PurchasePage);

        //    var data = new 
        //    {
        //        _bankConfig.TerminalId,
        //        _bankConfig.MerchantId,
        //        Amount= 1000,
        //        SignData,
        //        _bankConfig.ReturnUrl,
        //        LocalDateTime = DateTime.Now,
        //        OrderId,
        //        //MultiplexingData = request.MultiplexingData
        //    };

        //    var res = CallApi<BankResultViewModel>(ipgUri, data);
        //    res.Wait();

        //    if (res != null && res.Result != null)
        //    {
        //        if (res.Result.ResCode == "0")
        //        {

        //            await _usersPaymentRepository.MapAddAsync(SetValue(res.Result.Token));

        //            Response.Redirect(string.Format("{0}/Purchase/Index?token={1}", _bankConfig.PurchasePage, res.Result.Token));
        //        }
        //        ViewBag.Message = res.Result.Description;
        //        return View();
        //    }

        //    return View();

        //    #region LocalMethods

        //    AddUserPaymentViewModel SetValue(string token)
        //    {
        //        return new AddUserPaymentViewModel()
        //        {
        //            Amount = data.Amount,
        //            DateTime = data.LocalDateTime,
        //            OrderId = data.OrderId,
        //            Token = token,
        //            UserId = UserId
        //        };
        //    }

        //    #endregion
        //}

        /// <summary>
        /// بازگشت از درگاه
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult CallBack(BankVerifyViewModel vm)
        {
            return View(vm);
        }

        /// <summary>
        /// تایید اطلاعات
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        //[HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyRequest(BankVerifyViewModel vm)
        {
            try
            {
                _logRepository.Add(new Log() { Text = $"Start => " });
                // گرفتن اطلاعات فاکتور بر اساس شناسه خرید و شناسه گاربری
                var model = _usersPaymentRepository.GetByCondition(a => a.OrderId == vm.OrderId && a.Token == vm.Token);

                await _usersPaymentRepository.ResultOrderCallBack(model.ShopOrderId.Value, model.OrderId, UserId);

                // رمز گذاری توکن
                var dataBytes = Encoding.UTF8.GetBytes(vm.Token);

                var symmetric = SymmetricAlgorithm.Create("TripleDes");
                symmetric.Mode = CipherMode.ECB;
                symmetric.Padding = PaddingMode.PKCS7;

                var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(_bankConfig.MerchantKey), new byte[8]);

                var signedData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

                var data = new
                {
                    token = vm.Token,
                    SignData = signedData
                };

                var ipgUri = string.Format("{0}/api/v0/Advice/Verify", _bankConfig.PurchasePage);

                var res = CallApi<BankCallBackResultViewModel>(ipgUri, data);
                _logRepository.Add(new Log() { Text = $"1 => " + ((res != null && res.Result != null) ? "" : "") });
                if (res != null && res.Result != null)
                {
                    await _usersPaymentRepository.ResultOrder(model.ShopOrderId.Value, model.OrderId, UserId, res.Result.Succeed, res.Result.ResCode);
                    _logRepository.Add(new Log() { Text = $"2 => " + res.Result.ResCode.ToString() });
                    if (res.Result.ResCode == "3000")
                    {
                        vm.VerifyResultData = res.Result;
                        res.Result.Succeed = true;
                        ViewBag.Success = res.Result.Description;

                        // تغییر وضعیت فاکتور از پیش خرید به خرید شده
                        await _shopOrderRepository.SuccessedOrder(model.ShopOrderId.Value, model.UserId);
                        // تغییر وضعیت سبد خرید
                        await _shopProductRepository.SuccessedOrder(model.ShopOrderId.Value, model.UserId);
                        // ثبت کردن اطلاعات در وضعیت 
                        await _shopOrderStatusRepository.InsertAsync(new ShopOrderStatusInsertViewModel()
                        {
                            Date = DateTime.Now,
                            ShopOrderId = model.ShopOrderId.Value,
                            Status = ShopOrderStatusSSOT.Ordered
                        });

                        //// ارسال اس ام اس به کاربر جهت ثبت سفارش
                        //var text = $"{model.OrderId};{DateTime.Now.ToPersianDay()}";
                        //var phoneNumber = _userRepository.GetByCondition(a => a.Id == model.UserId).PhoneNumber;

                        //var smsResult = _smsRestClient.SendByBaseNumber(text, phoneNumber, (int)SmsBaseCodeSSOT.SetOrder);

                        //// ارسال اس ام اس به مدیریت 
                        //var ResultTest = $"{DateTime.Now.ToPersianDay()};{model.OrderId}";

                        //var ResultSms = _smsRestClient.SendByBaseNumber(ResultTest, "09122013443", (int)SmsBaseCodeSSOT.Result);


                        _logRepository.Add(new Log() { Text = $"3 => " + "Start send message" });
                        // ارسال اس ام اس به کاربر جهت ثبت سفارش
                        var text = $"{model.ShopOrderId};{DateTime.Now.ToPersianDay()}";
                        var phoneNumber = _userRepository.GetByCondition(a => a.Id == model.UserId).PhoneNumber;

                        var smsResult = _smsRestClient.SendByBaseNumber(text, phoneNumber, (int)SmsBaseCodeSSOT.SetOrder);

                        // ارسال اس ام اس به مدیریت 
                        var ResultTest = $"{DateTime.Now.ToPersianDay()};{model.ShopOrderId}";


                        var sms1Result = _smsRestClient.SendByBaseNumber(ResultTest, "09122013443", (int)SmsBaseCodeSSOT.Result);
                        var sms2Result = _smsRestClient.SendByBaseNumber(ResultTest, "09351631398", (int)SmsBaseCodeSSOT.Result);
                        var sms3Result = _smsRestClient.SendByBaseNumber(ResultTest, "09104996615", (int)SmsBaseCodeSSOT.Result);



                        _logRepository.Add(new Log() { Text = $"sms -> FirstUser>{sms1Result.RetStatus}-{sms1Result.Value}-{sms1Result.StrRetStatus}" });
                        _logRepository.Add(new Log() { Text = $"sms -> SecondUser>{sms2Result.RetStatus}-{sms2Result.Value}-{sms2Result.StrRetStatus}" });
                        _logRepository.Add(new Log() { Text = $"sms -> ThirtUser>{sms3Result.RetStatus}-{sms3Result.Value}-{sms3Result.StrRetStatus}" });
                        _logRepository.Add(new Log() { Text = $"3 => " + "End send message" });

                        return RedirectToAction("Result", "UserOrder", new { orderId = res.Result.OrderId, shopOrderId = model.ShopOrderId });
                    }

                    ViewBag.Message = res.Result.Description;
                    return RedirectToAction("Result", "UserOrder", new { orderId = model.OrderId, shopOrderId = model.ShopOrderId });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.ToString();
                _logRepository.Add(new Log() { Text = $"Error => {ex.ToString()}" });
            }

            return Redirect("/");
        }




        /// <summary>
        /// تایید اطلاعات
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        //[HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyPaymentRequest(BankVerifyViewModel vm)
        {
            try
            {
                _logRepository.Add(new Log() { Text = $"*Start => " });
                // گرفتن اطلاعات فاکتور بر اساس شناسه خرید و شناسه گاربری
                var model = _usersPaymentRepository.GetByCondition(a => a.OrderId == vm.OrderId && a.Token == vm.Token);

                var result = await _shopOrderPaymentRepository.GetByConditionAsync(a => a.OrderId == vm.OrderId);

                if (result == null)
                {
                    TempData.AddResult(SweetAlertExtenstion.Error("متاسفانه اطلاعاتی با این شناسه خرید یافت نشد اگر هزینه ای از شما کسر شد طی 72 ساعت آینده به حسابتان واریز می شود"));
                    return Redirect("/");
                }

                await _usersPaymentRepository.ResultOrderCallBack(result.ShopOrderId, result.OrderId, UserId);

                // رمز گذاری توکن
                var dataBytes = Encoding.UTF8.GetBytes(vm.Token);

                var symmetric = SymmetricAlgorithm.Create("TripleDes");
                symmetric.Mode = CipherMode.ECB;
                symmetric.Padding = PaddingMode.PKCS7;

                var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(_bankConfig.MerchantKey), new byte[8]);

                var signedData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

                var data = new
                {
                    token = vm.Token,
                    SignData = signedData
                };

                var ipgUri = string.Format("{0}/api/v0/Advice/Verify", _bankConfig.PurchasePage);

                var res = CallApi<BankCallBackResultViewModel>(ipgUri, data);
                _logRepository.Add(new Log() { Text = $"*1 => " + ((res != null && res.Result != null) ? "" : "") });

                if (res != null && res.Result != null)
                {
                    await _usersPaymentRepository.ResultOrder(result.ShopOrderId, result.OrderId, UserId, res.Result.Succeed, res.Result.ResCode);
                    _logRepository.Add(new Log() { Text = $"*2 => " + res.Result.ResCode.ToString() });
                    if (res.Result.ResCode == "0")
                    {
                        vm.VerifyResultData = res.Result;
                        res.Result.Succeed = true;
                        ViewBag.Success = res.Result.Description;


                        await _shopOrderPaymentRepository.UpdateStatus(result.Id);

                        // ثبت کردن اطلاعات در وضعیت 
                        await _shopOrderStatusRepository.InsertAsync(new ShopOrderStatusInsertViewModel()
                        {
                            Date = DateTime.Now,
                            ShopOrderId = result.ShopOrderId,
                            Status = ShopOrderStatusSSOT.Ordered
                        });


                        if (await _shopOrderPaymentRepository.AllIsPay(result.ShopOrderId))
                        {
                            // تغییر وضعیت فاکتور از پیش خرید به خرید شده
                            await _shopOrderRepository.SuccessedOrder(result.ShopOrderId, model.UserId);
                            // تغییر وضعیت سبد خرید
                            await _shopProductRepository.SuccessedOrder(result.ShopOrderId, model.UserId);
                        }

                        _logRepository.Add(new Log() { Text = $"*3 => " + "Start send message" });
                        // ارسال اس ام اس به کاربر جهت ثبت سفارش
                        var text = $"{result.ShopOrderId};{DateTime.Now.ToPersianDay()}";
                        var phoneNumber = _userRepository.GetByCondition(a => a.Id == model.UserId).PhoneNumber;

                        var smsResult = _smsRestClient.SendByBaseNumber(text, phoneNumber, (int)SmsBaseCodeSSOT.SetOrder);

                        // ارسال اس ام اس به مدیریت 
                        var ResultTest = $"{DateTime.Now.ToPersianDay()};{result.ShopOrderId}";


                        var sms1Result = _smsRestClient.SendByBaseNumber(ResultTest, "09122013443", (int)SmsBaseCodeSSOT.Result);
                        var sms2Result = _smsRestClient.SendByBaseNumber(ResultTest, "09351631398", (int)SmsBaseCodeSSOT.Result);
                        var sms3Result = _smsRestClient.SendByBaseNumber(ResultTest, "09104996615", (int)SmsBaseCodeSSOT.Result);

                        _logRepository.Add(new Log() { Text = $"*sms -> FirstUser>{sms1Result.RetStatus}-{sms1Result.Value}-{sms1Result.StrRetStatus}" });
                        _logRepository.Add(new Log() { Text = $"*sms -> SecondUser>{sms2Result.RetStatus}-{sms2Result.Value}-{sms2Result.StrRetStatus}" });
                        _logRepository.Add(new Log() { Text = $"*sms -> ThirtUser>{sms3Result.RetStatus}-{sms3Result.Value}-{sms3Result.StrRetStatus}" });
                        _logRepository.Add(new Log() { Text = $"*3 => " + "End send message" });


                        await _treeRepository.CalculateRateTreeFromAmountAndInsert(result.PaymentAmount, model.UserId);
                        return RedirectToAction("Result", "UserOrder", new { orderId = res.Result.OrderId, shopOrderId = result.ShopOrderId });
                    }

                    ViewBag.Message = res.Result.Description;
                    return RedirectToAction("Result", "UserOrder", new { orderId = result.OrderId, shopOrderId = result.ShopOrderId });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.ToString();
                _logRepository.Add(new Log() { Text = $"*Error => {ex.ToString()}" });
            }

            return Redirect("/");
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
