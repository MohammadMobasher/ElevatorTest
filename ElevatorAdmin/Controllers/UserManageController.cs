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

namespace ElevatorAdmin.Controllers
{
    [ControllerRole("مدیریت کاربران")]
    public class UserManageController : BaseAdminController
    {
        private readonly UserRepository _userRepository;
        private readonly UsersRoleRepository _usersRoleRepository;
        private readonly RoleRepository _roleRepository;
        private readonly SmsService _smsService;

        public UserManageController
            (UserRepository userRepository
            , UsersRoleRepository usersRoleRepository
            , RoleRepository roleRepository
            , SmsService smsService
            , UsersAccessRepository usersAccessRepository) : base(usersAccessRepository)
        {
            _userRepository = userRepository;
            _usersRoleRepository = usersRoleRepository;
            _roleRepository = roleRepository;
            _smsService = smsService;
        }

        [ActionRole("صفحه مدیریت کاربران")]
        [HasAccess]
        public async Task<IActionResult> Index(UsersSearchViewModel searchModel)
        {
            this.PageSize = 15;
            var model = await _userRepository.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);
            
        }
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
        public async Task<IActionResult> UserChangeStatus(int userId)
        {
            var swMessage = await _userRepository.ChangeUserActivity(userId);

            TempData.AddResult(swMessage);

            return RedirectToAction(nameof(Index));
        }

        #endregion

        [AllowAccess]
        public async Task<IActionResult> ChangeUserPic()
        {
            Users user = await _userRepository.GetByIdAsync(this.UserId);

            return View(model:user.ProfilePic);
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
    }
}