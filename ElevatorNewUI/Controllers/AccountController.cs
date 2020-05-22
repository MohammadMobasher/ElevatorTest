using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Utilities;
using DataLayer.Entities.Users;
using DataLayer.SSOT;
using DataLayer.ViewModels.User;
using DNTPersianUtils.Core;
using Elevator.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Repos.User;

namespace ElevatorNewUI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserRepository _userRepository;
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        private readonly SmsRestClient _smsRestClient;
        public AccountController(SignInManager<Users> signInManager,
            UserManager<Users> userManager,
            UserRepository userRepository,
            SmsRestClient smsRestClient
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _smsRestClient = smsRestClient;
        }



        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string redirect)
        {
            var model = _userRepository.TableNoTracking.FirstOrDefault(a => a.UserName == vm.UserName || a.PhoneNumber == vm.UserName);

            if (model == null)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("کاربری با این نام کاربری یافت نشد!"));

                return RedirectToAction("Login");
            }

            var result = await _signInManager.PasswordSignInAsync(model, vm.Password, vm.IsRemember, false);

            if (result.Succeeded)
            {

                if (model.IsPhoneNumberConfirm == null || model.IsPhoneNumberConfirm == null)
                {
                    TempData.AddResult(SweetAlertExtenstion.Error("لطفا شماره تلفن خود را تایید کنید!"));

                    await _signInManager.SignOutAsync();

                    return RedirectToAction("AuthorizePhoneNumber", new { sec = model.SecurityStamp });
                }

                if (model.IsActive == false)
                {
                    await _signInManager.SignOutAsync();

                    TempData.AddResult(SweetAlertExtenstion.Error("اکانت شما مسدود شده است!"));

                    return RedirectToAction("Login");
                }


                // درصورتی که کاربر قبل از لاگین به آدرس صفحه ایی را وارد کرده بود که نیاز به لاگین داشته است
                // در این صورت باید به آن صفحه هدایت شود
                if (!string.IsNullOrEmpty(redirect) && Url.IsLocalUrl(redirect))
                    return Redirect(redirect);
                else
                    return RedirectToAction("Index", "Home");
            }

            TempData.AddResult(SweetAlertExtenstion.Error("کلمه عبور یا نام کاربری نادرست است"));
            return RedirectToAction("Login");
        }





        #region logout

        [Authorize]
        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();

            return Redirect("/");
        }

        #endregion


        //#region Register

        [AllowAnonymous]
        public IActionResult Register(string ReturnUrl = null)
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string ReturnUrl = null)
        {
            if (ModelState.IsValid)
            {
                if (!model.IsAccept)
                {
                    TempData.AddResult(SweetAlertExtenstion.Error("لطفا شرایط را مطالعه و تایید کنید"));

                    return View(model);
                }

                var user = AutoMapper.Mapper.Map<Users>(model);

                // درصورتی که چنین کاربری از قبل وجود نداشته باشد
                var userResult = await _userRepository.GetByConditionAsync(x => x.UserName == model.UserName);

                if (userResult == null)
                {
                    var isPhoneNumberExist = await _userRepository.GetByConditionAsync(x => x.PhoneNumber == model.PhoneNumber);
                    if (isPhoneNumberExist == null)
                    {
                        var resultCreatUser = await _userManager.CreateAsync(user, model.Password);

                        // درصورتیکه کاربر مورد نظر با موفقیت ثبت شد آن را لاگین میکنیم
                        if (resultCreatUser.Succeeded)
                        {
                            //await _signInManager.SignInAsync(user, isPersistent: false);
                            var activeCode = await _userRepository.GenerateCode(user.Id);

                            var text = activeCode.ToPersianNumbers();

                            var resultSms = _smsRestClient.SendByBaseNumber(text, user.PhoneNumber, (int)SmsBaseCodeSSOT.Register);

                            return RedirectToAction("AuthorizePhoneNumber", "Account", new { sec = user.SecurityStamp });
                        }
                        else
                        {
                            if (resultCreatUser.Errors.Any(a => a.Code.Contains("DuplicateEmail")))
                            {
                                ViewBag.ErrorMessages = "ایمیل وارد شده تکراری می باشد";
                            }

                            TempData.AddResult(SweetAlertExtenstion.Error("عملیات با خطا مواجه شد لطفا مجددا تلاش نمایید"));
                            return View(model);
                        }
                    }
                    else
                    {
                        TempData.AddResult(SweetAlertExtenstion.Error("شماره تلفن وارد شده تکراری می باشد"));
                        return View(model);
                    }
                }
                else
                {
                    TempData.AddResult(SweetAlertExtenstion.Error("چنین کاربری از قبل وجود دارد"));
                    return View(model);
                }
            }
            var errorMessage = ModelState.ExpressionsMessages();

            TempData.AddResult(SweetAlertExtenstion.Error(errorMessage));
            return View(model);
        }


        [Authorize]
        public IActionResult ChagePassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChagePassword(ChangePasswordViewModel vm)
        {
            try
            {
                var userId = int.Parse(User.Identity.FindFirstValue(ClaimTypes.NameIdentifier));
                var user = await _userRepository.GetByConditionAsync(a => a.Id == userId && a.IsActive);

                if (user == null)
                {
                    TempData.AddResult(SweetAlertExtenstion.Error("کاربر گرامی دسترسی شما محدود شده است لطفا با پشتیبانی تماس بگیرید"));

                    return RedirectToAction("Index", "Profile");
                }

                var result = await _userManager.ChangePasswordAsync(user, vm.CurrentPassword, vm.NewPassword);
                if (result.Succeeded)
                {
                    TempData.AddResult(SweetAlertExtenstion.Ok("رمز عبور با موفقیت تغییر یافت"));
                    return RedirectToAction("Index","Profile");
                }

            }
            catch (System.Exception)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("خطایی در سایت رخ داده است لطفا با پشتیبانی تماس بگیرید"));
                return RedirectToAction("Index", "Profile");
            }

            return View();
        }


        public async Task<IActionResult> AuthorizePhoneNumber(string sec)
        {
            var model = await _userRepository.TableNoTracking.FirstOrDefaultAsync(a => a.SecurityStamp == sec);

            ViewBag.PhoneNumber = model.PhoneNumber;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AuthorizePhoneNumber(PhoneNumberAuthorizeViewModel vm)
        {
            var model = _userRepository.TableNoTracking.FirstOrDefault(a => a.PhoneNumber == vm.PhoneNumber
            && a.ActiveCode == vm.ActiveCode);

            if (model == null)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("کد وارد شده معتبر نمی باشد"));
                return RedirectToAction("Login");
            }

            if (model.ExpireTime < DateTime.Now)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("تاریخ انقضای این کد گذشته است!"));
                return RedirectToAction("AuthorizePhoneNumber", new { sec = model.SecurityStamp });
            }

            TempData.AddResult(await _userRepository.PhoneNumberConfirmed(model.Id));
            await _signInManager.SignInAsync(model, isPersistent: false);
            await _userRepository.ChangeCode(model.Id);

            return Redirect("/");
        }

        public async Task<IActionResult> ResendCode(string phoneNumber)
        {
            var model = _userRepository.TableNoTracking.FirstOrDefault(a => a.PhoneNumber == phoneNumber);

            if (model == null)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("شماره تماس وارد شده معتبر نمی باشد"));

                return RedirectToAction("AuthorizePhoneNumber", new { sec = model.SecurityStamp });

            }

            if (model.ExpireTime > DateTime.Now)
            {
                var text = model.ActiveCode.ToPersianNumbers();

                var smsResult = _smsRestClient.SendByBaseNumber(text, model.PhoneNumber, (int)SmsBaseCodeSSOT.Register);
            }
            else
            {
                var activeCode = await _userRepository.GenerateCode(model.Id);

                var smsResult = _smsRestClient.SendByBaseNumber(activeCode.ToPersianNumbers(), model.PhoneNumber, (int)SmsBaseCodeSSOT.Register);

            }

            return RedirectToAction("AuthorizePhoneNumber", new { sec = model.SecurityStamp });
        }


        #region ForgetPassword

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel vm)
        {
            var model = await _userRepository
                .GetByConditionAsync(a => a.UserName == vm.UserName && a.PhoneNumber == vm.PhoneNumber);

            if (model == null)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("کاربری یافت نشد"));
                return View();
            }

            var resultSms = _smsRestClient.SendByBaseNumber(model.ActiveCode.ToPersianNumbers(), model.PhoneNumber, (int)SmsBaseCodeSSOT.ForgetPassword);

            return RedirectToAction("AuthorizeCode", new { code = model.SecurityStamp });
        }

        public IActionResult AuthorizeCode(string code)
        {
            ViewBag.Code = code;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AuthorizeCode(int activeCode, string code)
        {
            var model = await _userRepository.GetByConditionAsync(a => a.SecurityStamp == code);

            if (model.ActiveCode != activeCode)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("کد تاییده صحیح نمی باشد"));

                return View(code);
            }

            await _userRepository.ChangeCode(model.Id);

            return RedirectToAction("ResetPassword", new { code });
        }

        public async Task<IActionResult> ResetPassword(string code)
        {
            ViewBag.Code = code;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = await _userRepository.GetByConditionAsync(a => a.SecurityStamp == vm.Code);

                if (model == null)
                {
                    TempData.AddResult(SweetAlertExtenstion.Error("اطلاعات وارد شده مغایرت دارد"));
                    return RedirectToAction("Login");
                }
                var password = _userManager.PasswordHasher.HashPassword(model, vm.NewPassword);

                model.PasswordHash = password;

                _userRepository.Update(model);

                TempData.AddResult(SweetAlertExtenstion.Ok("رمز عبور شما با موفقیت ویرایش شد"));

                return RedirectToAction("Login");
            }

            TempData.AddResult(SweetAlertExtenstion.Error("رمز عبور با تکرارش مغایرت دارد"));
            return RedirectToAction("ResetPassword", new { code = vm.Code });
        }

        #endregion
    }
}