using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO.UserDTO;
using DataLayer.Entities.Users;
using DataLayer.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;
using WebFramework.SmsManage;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Http;
using DataLayer.DTO.RolesDTO;
using System;
namespace ElevatorAdmin.Controllers
{
    [ControllerRole("مدیریت کاربران")]
    public class UserManageController : BaseAdminController
    {
        private readonly UserRepository _userRepository;
        private readonly UserManager<Users> _userManager;
        private readonly UsersRoleRepository _usersRoleRepository;
        private readonly RoleRepository _roleRepository;
        private readonly SmsService _smsService;

        public UserManageController
            (UserRepository userRepository,
            UserManager<Users> userManager

            , UsersRoleRepository usersRoleRepository
            , RoleRepository roleRepository
            , SmsService smsService
            , UsersAccessRepository usersAccessRepository) : base(usersAccessRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _usersRoleRepository = usersRoleRepository;
            _roleRepository = roleRepository;
            _smsService = smsService;
        }

        [ActionRole("صفحه مدیریت کاربران")]
        [HasAccess]
        public async Task<IActionResult> Index(UsersSearchViewModel searchModel)
        {

            var model = await _userRepository.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);

        }

        #region ثبت کاربر جدید 
        [ActionRole("ثبت کاربر جدید")]
        [HasAccess]
        public async Task<IActionResult> NewUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewUser(RegisterUserAdminViewModel vm)
        {

            if (vm.Password != vm.RePassword)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("کلمه عبور با تکرار آن یکسان نیست"));
                return RedirectToAction("Index");
            }

            var user = AutoMapper.Mapper.Map<Users>(vm);
            user.CreateDate = DateTime.Now;
            user.IsActive = true;



            // درصورتی که چنین کاربری از قبل وجود نداشته باشد
            var userResult = await _userRepository.GetByConditionAsync(x => x.UserName == vm.UserName);


            if (userResult == null)
            {
                var userResultPhoneNumber = await _userRepository.GetByConditionAsync(x => x.PhoneNumber == vm.PhoneNumber);
                if (userResultPhoneNumber == null)
                {
                    var resultCreatUser = await _userManager.CreateAsync(user, vm.Password);
                    // درصورتیکه کاربر مورد نظر با موفقیت ثبت شد آن را لاگین میکنیم
                    if (resultCreatUser.Succeeded)
                    {
                        TempData.AddResult(SweetAlertExtenstion.Ok());
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData.AddResult(SweetAlertExtenstion.Error("کاربری با این شماره تلفن از قبل وجود دارد"));
                    return RedirectToAction("Index");
                }
                //else
                //{
                //    if (resultCreatUser.Errors.Any(a => a.Code.Contains("DuplicateEmail")))
                //    {
                //        ViewBag.ErrorMessages = "ایمیل وارد شده تکراری می باشد";
                //    }

                //    TempData.AddResult(SweetAlertExtenstion.Error("عملیات با خطا مواجه شد لطفا مجددا تلاش نمایید"));
                //    return View(model);
                //}
            }
            else
            {
                TempData.AddResult(SweetAlertExtenstion.Error("چنین کاربری از قبل وجود دارد"));
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region دسترسی دادن به کاربران

        [ActionRole("نقش دادن به کاربر")]
        [HasAccess]
        public IActionResult SetRole(int id)
        {
            // لیست تمامی نقش های تعریف شده در سایت
            ViewBag.Roles = _roleRepository.TableNoTracking.ToList();
            // شماره کاربری 
            ViewBag.UserId = id;

            //  نقش کاربر انتخاب شده
            var userRole = _usersRoleRepository.GetRolesByUserId(id);

            return View(userRole);
        }



        [ActionRole("ثبت نقش جدید برای کاربر")]
        [HasAccess]
        public async Task<IActionResult> SetRoleInsert(int UserId)
        {
            // لیست تمامی نقش های تعریف شده در سایت
            ViewBag.Roles = await _roleRepository.LoadAsync<RolesDTO>();

            return View(UserId);
        }

        [HttpPost]
        public async Task<IActionResult> SetRoleInsert(SetUserRoleViewModel vm)
        {
            var swMessage = _usersRoleRepository.SetRole(vm);

            TempData.AddResult(swMessage);

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region ویرایش رمز عبور

        [ActionRole("ویرایش رمز عبور")]
        [HasAccess]
        public IActionResult EditPassword(int id) => View(id);

        [HttpPost]
        public async Task<IActionResult> EditPassword(AdminSetPasswordViewModel vm)
        {
            var sweetMessage = await _userRepository.AdminChangePassword(vm);

            TempData.AddResult(sweetMessage);

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region ارسال پیامک به کاربر

        [ActionRole("ارسال پیامک به کاربر")]
        [HasAccess]
        public IActionResult SendSmsToUser(int id)
        {
            //var credit = _smsService.Credit();

            return View(id);
        }

        [HttpPost]
        public async Task<IActionResult> SendSmsToUser(SendSmsToUserViewModel vm)
        {
            var model = await _userRepository.PhoneNumberByUserId(vm.UserId);

            var swMessage = _smsService.SendSms(model, vm.Message);

            TempData.AddResult(swMessage);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region ارسال پیامک به تمامی کاربران

        [ActionRole("ارسال پیامک به تمامی کاربران")]
        [HasAccess]
        public IActionResult SendSmsToUsers()
        {
            var countAllUser = _userRepository.CountUsers();
            //var credit = _smsService.Credit();

            return View(countAllUser);
        }

        [HttpPost]
        public async Task<IActionResult> SendSmsToUsers(SendSmsToAllUsersViewModel vm)
        {
            var phoneNumbers = await _userRepository.AllUserPhoneNumber(vm.SendType);

            var swMessage = _smsService.SendSmsRange(phoneNumbers, vm.Message);

            TempData.AddResult(swMessage);

            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region فعال/غیرفعال کردن کاربر

        [ActionRole("فعال/غیرفعال کردن کاربر")]
        [HasAccess]

        public async Task<IActionResult> UserChangeStatus(int id)
        {
            var swMessage = await _userRepository.ChangeUserActivity(id);

            TempData.AddResult(swMessage);

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region تغییر عکس کاربر

        [AllowAccess]
        public async Task<IActionResult> ChangeUserPic()
        {
            Users user = await _userRepository.GetByIdAsync(this.UserId);

            return View(model: user.ProfilePic);
        }

        [HttpPost]
        [AllowAccess]
        public async Task<IActionResult> ChangeUserPic(IFormFile ProfilePic)
        {
            if (ProfilePic != null)
            {
                await _userRepository.UpdateProfilePic(this.UserId, ProfilePic);
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region ایجاد کاربر

        public async Task<IActionResult> CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(AdminRegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = AutoMapper.Mapper.Map<Users>(model);

                var userResult = await _userRepository.GetByConditionAsync(x => x.UserName == model.UserName );
                var isPhoneNumberExist = await _userRepository.GetByConditionAsync(x => x.PhoneNumber == model.PhoneNumber);

                if (userResult == null)
                {
                    if (isPhoneNumberExist == null)
                    {
                        var resultCreatUser = await _userManager.CreateAsync(user, "liftbazar123");
                        // درصورتیکه کاربر مورد نظر با موفقیت ثبت شد آن را لاگین میکنیم
                        if (resultCreatUser.Succeeded)
                        {
                            TempData.AddResult(SweetAlertExtenstion.Ok());
                            return RedirectToAction("Index", "UserManage");
                        }
                        else
                        {
                            if (resultCreatUser.Errors.Any(a => a.Code.Contains("DuplicateEmail")))
                            {
                                ViewBag.ErrorMessages = "ایمیل وارد شده تکراری می باشد";
                            }

                            TempData.AddResult(SweetAlertExtenstion.Error("ایمیل وارد شده تکراری می باشد"));
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

        #endregion
    }
}