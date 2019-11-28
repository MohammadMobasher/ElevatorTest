using System.Linq;
using AutoMapper.QueryableExtensions;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO.UserDTO;
using DataLayer.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;
using WebFramework.SmsManage;

namespace ElevatorAdmin.Controllers
{
    [ControllerRole("مدیریت کاربران")]
    public class UserManageController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly UsersRoleRepository _usersRoleRepository;
        private readonly RoleRepository _roleRepository;
        private readonly SmsService _smsService;

        public UserManageController
            (UserRepository userRepository
            , UsersRoleRepository usersRoleRepository
            , RoleRepository roleRepository
            , SmsService smsService)
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

        [ActionRole("ثبت کاربر جدید")]
        [HasAccess]
        public IActionResult Create()
        {
            return View();
        }

        [ActionRole("ویرایش کاربر")]
        [HasAccess]
        public IActionResult Edit()
        {
            return View();
        }

        [ActionRole("تست")]
        [HasAccess]
        public IActionResult Test()
        {
            return View();
        }

    }
}