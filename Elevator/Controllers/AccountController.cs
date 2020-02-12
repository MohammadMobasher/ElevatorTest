using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Utilities;
using DataLayer.Entities.Users;
using DataLayer.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.User;

namespace Elevator.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserRepository _userRepository;

        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;

        public AccountController(SignInManager<Users> signInManager,
            UserManager<Users> userManager,
            UserRepository userRepository
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
        }



        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string redirect)
        {
            var model = _userRepository.TableNoTracking.FirstOrDefault(a => a.UserName == vm.UserName);

            if (model == null)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("کاربری با این نام کاربری یافت نشد!"));

                return RedirectToAction("Login");
            }

            var result = await _signInManager.PasswordSignInAsync(model, vm.Password, vm.IsRemember, false);

            if (result.Succeeded)
            {
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
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return RedirectToAction("Index", "Home");
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
        public async Task<IActionResult> ChagePassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChagePassword(ChangePasswordViewModel vm)
        {
            try
            {
                var userId = int.Parse(User.Identity.FindFirstValue(ClaimTypes.NameIdentifier));
                var user =await _userRepository.GetByConditionAsync(a=>a.Id == userId && a.IsActive);

                if(user == null)
                {
                    TempData.AddResult(SweetAlertExtenstion.Error("کاربر گرامی دسترسی شما محدود شده است لطفا با پشتیبانی تماس بگیرید"));

                    return Redirect("/");
                }

                var result = await _userManager.ChangePasswordAsync(user, vm.CurrentPassword, vm.NewPassword);
                if (result.Succeeded)
                {
                    TempData.AddResult(SweetAlertExtenstion.Ok("رمز عبور با موفقیت تغییر یافت"));
                    return Redirect("/");
                }

            }
            catch (System.Exception)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("خطایی در سایت رخ داده است لطفا با پشتیبانی تماس بگیرید"));
                return Redirect("/");
            }

            return View();
        }
    }
}