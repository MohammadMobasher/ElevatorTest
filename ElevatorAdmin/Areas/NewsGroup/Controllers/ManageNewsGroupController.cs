using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO;
using DataLayer.ViewModels.NewsGroup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.NewsGroup.Controllers
{
    [Area("NewsGroup")]
    [ControllerRole("مدیریت گروه اخبار")]
    public class ManageNewsGroupController : BaseAdminController
    {
        private readonly NewsGroupRepository _newsGroupRepository;

        public ManageNewsGroupController(NewsGroupRepository newsGroupRepository, UsersAccessRepository usersAccessRepository) : base(usersAccessRepository)
        {
            
            _newsGroupRepository = newsGroupRepository;
        }


        [ActionRole("صفحه لیست گروه اخبار")]
        [HasAccess]
        public async Task<IActionResult> Index(NewsGroupSearchViewModel searchModel)
        {
            this.PageSize = 15;
            var model = await _newsGroupRepository.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize, 
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);
        }


        #region ثبت

        [ActionRole("ثبت گروه جدید")]
        [HasAccess]
        public async Task<IActionResult> Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(NewsGroupInsertViewModel model)
        {
            var result = await _newsGroupRepository.AddAsync(model);

            TempData.AddResult(result);
            
            return RedirectToAction("Index");
        }

        #endregion

        #region ویرایش
        [ActionRole("ویرایش گروه")]
        [HasAccess]
        public async Task<IActionResult> Update(int Id)
        {

            var result = await _newsGroupRepository.GetByIdAsync(Id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(NewsGroupUpdateViewModel model)
        {
            var result = await _newsGroupRepository.UpdateAsync(model);

            TempData.AddResult(result);

            return RedirectToAction("Index");
        }

        #endregion

        #region حذف
        [ActionRole("حذف گروه")]
        [HasAccess]
        public async Task<IActionResult> Delete(int Id)
        {

            var result = await _newsGroupRepository.DeleteAsync(Id);
            TempData.AddResult(result);

            return RedirectToAction("Index");
        }

        #endregion
    }
}