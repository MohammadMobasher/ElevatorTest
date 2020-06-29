using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO;
using DataLayer.ViewModels;
using DataLayer.ViewModels.LogoManufactory;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.LogoManufactory
{
    [Area("LogoManufactory")]
    [ControllerRole("مدیریت لوگو کارخانه‌ها")]
    public class ManageLogoManufactoryController : BaseAdminController
    {
        private readonly LogoManufactoryRepository _logoManufactoryRepository;

        public ManageLogoManufactoryController(UsersAccessRepository usersAccessRepository,
            LogoManufactoryRepository logoManufactoryRepository) : base(usersAccessRepository)
        {
            _logoManufactoryRepository = logoManufactoryRepository;
        }



        [ActionRole("صفحه لیست لوگوها")]
        [HasAccess]
        public async Task<IActionResult> Index(LogoManufactorySearchViewModel searchModel)
        {
            var model = await _logoManufactoryRepository.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);
        }



        #region ثبت
        [ActionRole("ثبت لوگو جدید")]
        [HasAccess]
        public async Task<IActionResult> Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(InsertLogoManufactoryListView model)
        {
            TempData.AddResult(await _logoManufactoryRepository.Insert(model));

            return Redirect(IndexUrlWithQueryString);
        }

        #endregion

        #region ویرایش
        [ActionRole("ویرایش لوگو")]
        [HasAccess]
        public async Task<IActionResult> Update(int Id)
        {

            var result = await _logoManufactoryRepository.GetByIdAsync(Id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateLogoManufactoryViewModel model)
        {
            var result = await _logoManufactoryRepository.Update(model);

            TempData.AddResult(result);

            return Redirect(IndexUrlWithQueryString);
        }

        #endregion


        #region حذف

        [ActionRole("حذف آیتم")]
        [HasAccess]
        public async Task<IActionResult> Delete(int Id)
        {

            return View(new DeleteDTO { Id = Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {

            var result = await _logoManufactoryRepository.Delete(model.Id);
            TempData.AddResult(result);

            return Redirect(IndexUrlWithQueryString);
        }

        #endregion
    }
}