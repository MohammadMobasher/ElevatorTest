using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities;
using DataLayer.DTO;
using DataLayer.ViewModels.NewsGroup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.NewsGroup.Controllers
{
    [Area("NewsGroup")]
    [AllowAnonymous()]
    public class ManageNewsGroupController : BaseAdminController
    {
        private readonly NewsGroupRepository _newsGroupRepository;

        public ManageNewsGroupController(NewsGroupRepository newsGroupRepository)
        {
            
            _newsGroupRepository = newsGroupRepository;
        }

        public async Task<IActionResult> Index(NewsGroupSearchViewModel searchModel)
        {
            this.PageSize = 10;
            var model = await _newsGroupRepository.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize, 
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);
        }


        #region ثبت

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

        public async Task<IActionResult> Delete(int Id)
        {

            var result = await _newsGroupRepository.DeleteAsync(Id);
            TempData.AddResult(result);

            return RedirectToAction("Index");
        }

        #endregion
    }
}