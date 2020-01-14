using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO;
using DataLayer.ViewModels;
using DataLayer.ViewModels.Condition;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.Condition.Controllers
{

    [Area("Condition")]
    [ControllerRole("مدیریت شروط")]
    public class ManageConditionController : BaseAdminController
    {
        private readonly ConditionRepository _conditionRepository;

        public ManageConditionController(UsersAccessRepository usersAccessRepository,
            ConditionRepository conditionRepository) : base(usersAccessRepository)
        {
            _conditionRepository = conditionRepository;
        }

        [ActionRole("صفحه لیست وابستگی‌ها")]
        [HasAccess]
        public async Task<IActionResult> Index(ConditionSearchViewModel searchModel)
        {
            var model = await _conditionRepository.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;
            
            return View(model.Item2);
        }


        #region ثبت

        [ActionRole("ثبت شرط جدید")]
        [HasAccess]
        public async Task<IActionResult> Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ConditionInsertViewModel model)
        {
            TempData.AddResult(await _conditionRepository.InsertAsync(model));
            return RedirectToAction("Index");
        }

        #endregion


        #region ویرایش

        [ActionRole("ویرایش شرط")]
        [HasAccess]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _conditionRepository.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ConditionUpdateViewModel model)
        {
            TempData.AddResult(await _conditionRepository.UpdateAsync(model));
            return RedirectToAction("Index");
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

            var result = await _conditionRepository.DeleteAsync(model.Id);
            TempData.AddResult(result);

            return RedirectToAction("Index");
        }

        #endregion
    }
}