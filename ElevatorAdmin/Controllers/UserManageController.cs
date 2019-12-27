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
        public IActionResult Index()
        {
            var model = _userRepository
                .TableNoTracking
                .ProjectTo<UsersManageDTO>()
                .ToList();



            return View(model);
        }
        #region دسترسی دادن به کاربران

        [ActionRole("دسترسی دادن به کاربر")]
        [HasAccess]
        public IActionResult SetRole(int id)
        {
            // لیست تمامی نقش های تعریف شده در سایت
            ViewBag.Roles = _roleRepository.TableNoTracking.ToList();
            //  نقش کاربر انتخاب شده
            var userRole = _usersRoleRepository.TableNoTracking.FirstOrDefault(a => a.UserId == id);


            return View(userRole);
        }

        [HttpPost]
        public IActionResult SetRole(SetUserRoleViewModel vm)
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
            return View();
        }
    }
}