using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.ViewModels.SiteSetting;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.SiteSetting.Controllers
{
    [Area("SiteSetting")]
    [ControllerRole("مدیریت تنظیمات سایت")]
    public class ManageSiteSettingController : BaseAdminController
    {
        private readonly SiteSettingRepository _siteSettingRepository;

        public ManageSiteSettingController(UsersAccessRepository usersAccessRepository,
            SiteSettingRepository siteSettingRepository) : base(usersAccessRepository)
        {
            _siteSettingRepository = siteSettingRepository;
        }

        [ActionRole("ویرایش تنظیمات سایت")]
        [HasAccess]
        public async Task<IActionResult> Index()
        {
            var model = await _siteSettingRepository.GetInfo();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SiteSettingInsertViewModel vm)
        {
            TempData.AddResult(await _siteSettingRepository.UpdateInfo(vm));
            return RedirectToAction("Index");
        }
    }
}