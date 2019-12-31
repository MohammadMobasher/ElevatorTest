using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.DTO;
using DataLayer.ViewModels.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using WebFramework.Base;
using Core.Utilities;
using System.Security.Claims;
using Core.CustomAttributes;
using WebFramework.Authenticate;
using Service.Repos.User;

namespace ElevatorAdmin.Areas.News.Controllers
{
    [Area("News")]
    [ControllerRole("مدیریت اخبار")]
    public class ManageNewsController : BaseAdminController
    {

        private readonly NewsRepository _newsRepository;
        private readonly NewsGroupRepository _newsGroupRepository;

        public ManageNewsController(NewsRepository newsRepository,
            NewsGroupRepository newsGroupRepository, UsersAccessRepository usersAccessRepository) : base(usersAccessRepository)
        {
            _newsRepository = newsRepository;
            _newsGroupRepository = newsGroupRepository;


        }

        [ActionRole("صفحه لیست اخبار")]
        [HasAccess]
        public async Task<IActionResult> Index(NewsSearchViewModel searchModel)
        {
            var model = await _newsRepository.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);

        }


        #region ثبت
        [ActionRole("ثبت خبر جدید")]
        [HasAccess]
        public async Task<IActionResult> Insert()
        {
            var NewsGroups = await _newsGroupRepository.LoadAsync<NewsGroupDTO>();

            return View(NewsGroups);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(InsertNewsListView model)
        {
            model.UserId = this.UserId;

            TempData.AddResult(await _newsRepository.Insert(model));

            return RedirectToAction("Index");
        }

        #endregion

        #region ویرایش
        [ActionRole("ویرایش خبر")]
        [HasAccess]
        public async Task<IActionResult> Update(int Id)
        {

            var result = await _newsRepository.GetByIdAsync(Id);
            ViewBag.NewsGroups = await _newsGroupRepository.LoadAsync<NewsGroupDTO>();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(NewsUpdateViewModel model)
        {
            var result = await _newsRepository.UpdateAsync(model);

            TempData.AddResult(result);

            return RedirectToAction("Index");
        }

        #endregion

        #region حذف
        [ActionRole("حذف خبر")]
        [HasAccess]
        public async Task<IActionResult> Delete(int Id)
        {

            var result = await _newsRepository.DeleteAsync(Id);
            TempData.AddResult(result);

            return RedirectToAction("Index");
        }

        #endregion
    }
}