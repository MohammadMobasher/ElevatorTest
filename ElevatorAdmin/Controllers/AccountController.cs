using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.Entities.Users;
using DataLayer.SSOT;
using DataLayer.ViewModels.User;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Repos.User;
using WebFramework.Base;

namespace ElevatorAdmin.Controllers
{
    public class AccountController : BaseAdminController
    {
        private readonly UserRepository _userRepository;
        private readonly UsersRoleRepository _usersRoleRepository;
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        private readonly RoleRepository _roleRepository;
        private readonly SmsRestClient _smsRestClient;
        
        public AccountController(SignInManager<Users> signInManager,
            UserManager<Users> userManager,
            UserRepository userRepository,
            UsersRoleRepository usersRoleRepository,
            RoleRepository roleRepository,
            UsersAccessRepository usersAccessRepository,
            SmsRestClient smsRestClient
            ) : base(usersAccessRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _usersRoleRepository = usersRoleRepository;
            _roleRepository = roleRepository;
            _smsRestClient = smsRestClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAccess]
        [AllowAnonymous()]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [AllowAccess]
        [AllowAnonymous()]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var model = _userRepository.TableNoTracking.FirstOrDefault(a => a.UserName == userName);

            if (model == null)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("کاربری با این نام کاربری یافت نشد!"));
                return RedirectToAction("Login");
            }

            if (model.IsActive == false)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("شما فعال نیستید!"));
                return RedirectToAction("Login");
            }

            if(model.IsModerator == false)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("کاربری با این نام کاربری یافت نشد!"));
                return RedirectToAction("Login");
            }
            var result = await _signInManager.PasswordSignInAsync(model, password, true, false);

            if (result.Succeeded)
            {

                //Request.Headers["User-Agent"].ToString();

                //await _userRepository.SetUserClaims(userName);
                return RedirectToAction("Index", "Home");
            }
            TempData.AddResult(SweetAlertExtenstion.Error("کلمه عبور یا نام کاربری نادرست است"));
            
            return RedirectToAction("Index");
        }

        [AllowAccess]
        [AllowAnonymous()]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        #region ForgetPassword
        [AllowAnonymous()]
        [AllowAccess]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous()]
        [AllowAccess]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel vm)
        {
            var model = await _userRepository
                .GetByConditionAsync(a => a.UserName == vm.UserName && a.PhoneNumber == vm.PhoneNumber);

            if (model == null)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("کاربری یافت نشد"));
                return View();
            }
            var text = "param1;param2";

            var resultSms = _smsRestClient.SendByBaseNumber(model.ActiveCode.ToPersianNumbers(), model.PhoneNumber, (int)SmsBaseCodeSSOT.ForgetPassword);

            return RedirectToAction("AuthorizeCode", new { code = model.SecurityStamp });
        }
        [AllowAnonymous()]
        [AllowAccess]
        public IActionResult AuthorizeCode(string code)
        {
            ViewBag.Code = code;
            return View();
        }

        [HttpPost]
        [AllowAnonymous()]
        [AllowAccess]
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
        [AllowAnonymous()]
        [AllowAccess]
        public async Task<IActionResult> ResetPassword(string code)
        {
            ViewBag.Code = code;
            return View();
        }

        [HttpPost]
        [AllowAccess]
        [AllowAnonymous()]
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
