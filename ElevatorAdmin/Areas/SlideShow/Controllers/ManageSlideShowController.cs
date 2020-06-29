using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO;
using DataLayer.ViewModels;
using DataLayer.ViewModels.SlideShow;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.SlideShow.Controllers
{
    [Area("SlideShow")]
    [ControllerRole("مدیریت اسلایدشو")]
    public class ManageSlideShowController : BaseAdminController
    {
        private readonly SlideShowRepository _slideShowRepository;

        public ManageSlideShowController(UsersAccessRepository usersAccessRepository,
            SlideShowRepository slideShowRepository) : base(usersAccessRepository)
        {
            _slideShowRepository = slideShowRepository;
        }

        [ActionRole("صفحه لیست اسلایدشو")]
        [HasAccess]
        public async Task<IActionResult> Index(SlideShowSearchViewModel searchModel)
        {
            var model = await _slideShowRepository.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);

        }



        #region ثبت
        [ActionRole("ثبت اسلایدشو جدید")]
        [HasAccess]
        public async Task<IActionResult> Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(SlideShowInsertViewModel model)
        {
            TempData.AddResult(await _slideShowRepository.AddAsync(model));

            return Redirect(IndexUrlWithQueryString);
        }

        #endregion

        #region ویرایش
        [ActionRole("ویرایش آیتم")]
        [HasAccess]
        public async Task<IActionResult> Update(int Id)
        {

            var result = await _slideShowRepository.GetByIdAsync(Id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SlideShowUpdateViewModel model)
        {
            var result = await _slideShowRepository.UpdateAsync(model);

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

            var result = await _slideShowRepository.DeleteAsync(model.Id);
            TempData.AddResult(result);

            return Redirect(IndexUrlWithQueryString);
        }

        #endregion
    }
}