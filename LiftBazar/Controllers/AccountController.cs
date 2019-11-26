using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extenstions;
using DataLayer.Entities.Users;
using DataLayer.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Repos.User;

namespace LiftBazar.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository _userRepository;

        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        //private readonly IMapper _mapper;

        public AccountController(
            SignInManager<Users> signInManager,
            UserManager<Users> userManager,
            UserRepository userRepository
            //IMapper mapper
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
            //_mapper = mapper;
        }


        
        public IActionResult Login()
        {
            TempData.AddResult(SweetAlertExtenstion.Error());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm,string redirect)
        {
            var model = _userRepository.TableNoTracking.FirstOrDefault(a => a.UserName == vm.UserName);

            if (model == null)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("کاربری با این نام کاربری یافت نشد"));

                return RedirectToAction("Login");
            }

            var result = await _signInManager.PasswordSignInAsync(model.UserName, vm.Password, vm.IsRemember, false);

            if (result.Succeeded)
            {
                // درصورتی که کاربر قبل از لاگین به آدرس صفحه ایی را وارد کرده بود که نیاز به لاگین داشته است
                // در این صورت باید به آن صفحه هدایت شود
                if (!string.IsNullOrEmpty(redirect) && Url.IsLocalUrl(redirect))
                    return Redirect(redirect);
                else
                    return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Login");
        }


      

        
        //#region LogOut

        //[Authorize]
        //public async Task<IActionResult> LogOut()
        //{

        //    await _signInManager.SignOutAsync();

        //    return RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
        //}

        //#endregion


        //#region Register

        //[AllowAnonymous]
        //public async Task<IActionResult> Register(string ReturnUrl = null)
        //{
        //    return View();
        //}

        //[AllowAnonymous]
        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel model, string ReturnUrl = null)
        //{
        //    //===============================================
        //    model.IsUser = true;
        //    var user = _mapper.Map<User>(model);
        //    user.UserName = model.Email;
        //    //===============================================

        //    if (model.Password == model.ConfirmPassword)
        //    {

        //        var userResult = _userRepository.Get(x => x.Email == model.Email);
        //        // درصورتی که چنین کاربری از قبل وجود نداشته باشد
        //        if (userResult == null && userResult.Count() != 0)
        //        {
        //            var resultCreatUser = await _userManager.CreateAsync(user, model.Password);
        //            // درصورتیکه کاربر مورد نظر با موفقیت ثبت شد آن را لاگین میکنیم
        //            if (resultCreatUser.Succeeded)
        //            {
        //                await _signInManager.SignInAsync(user, isPersistent: false);
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                ViewBag.Message = "عملیات با خطا مواجه شد لطفا مجددا تلاش نمایید";
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.Message = "چنین کاربری از قبل وجود دارد";
        //        }
        //    }
        //    else
        //        ViewBag.Message = "کلمه عبور و تکرار کلمه عبور باید مثل هم باشند";
        //    return View();
        //}

        //#endregion

        //[AllowAnonymous]
        //public IActionResult Test()
        //{
        //    return View();
        //}
    }
}