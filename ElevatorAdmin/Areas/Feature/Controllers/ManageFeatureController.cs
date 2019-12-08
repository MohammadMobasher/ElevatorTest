using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO;
using DataLayer.ViewModels;
using DataLayer.ViewModels.Feature;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.Feature.Controllers
{
    [Area("Feature")]
    [ControllerRole("مدیریت ویژگی‌ها")]
    public class ManageFeatureController : BaseAdminController
    {
        private readonly FeatureRepository _featureRepository;
        private readonly FeatureItemRepository _featureItemRepository;

        public ManageFeatureController(FeatureRepository featureRepository
            , FeatureItemRepository featureItemRepository
            , UsersAccessRepository usersAccessRepository) : base(usersAccessRepository)
        {
            _featureRepository = featureRepository;
            _featureItemRepository = featureItemRepository;
        }


        [ActionRole("صفحه لیست کاربران")]
        [HasAccess]
        public async Task<IActionResult> Index(FeatureSearchViewModel searchModel)
        {

            this.PageSize = 10;
            var model = await _featureRepository.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);
        }


        #region ثبت


        [ActionRole("ثبت ویژگی جدید")]
        [HasAccess]
        public async Task<IActionResult> Insert()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(FeatureInsertViewModel model, FeatureItemsViewModel vm)
        {
            if (model.FeatureType != DataLayer.SSOT.FeatureTypeSSOT.Fssot)
            {
                TempData.AddResult(await _featureRepository.Insert(model));
                return RedirectToAction("Index");
            }
            
            var featureId = await _featureRepository.InsertFeature(model);
            vm.FeatureId = featureId.Value;
            TempData.AddResult(await _featureItemRepository.InsertFeatureItem(vm));
            return RedirectToAction("Index");
        }

        #endregion


        #region ویرایش


        [ActionRole("ویرایش ویژگی")]
        [HasAccess]
        public async Task<IActionResult> Update(int Id)
        {
            var result = await _featureRepository.GetByIdAsync(Id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(FeatureUpdateViewModel model)
        {

            TempData.AddResult(await _featureRepository.UpdateAsync(model));

            return RedirectToAction("Index");
        }

        #endregion


        #region حذف

        [ActionRole("حذف ویژگی")]
        [HasAccess]
        public async Task<IActionResult> Delete(int Id)
        {

            return View(new DeleteDTO { Id = Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {

            var result = await _featureRepository.DeleteAsync(model.Id);
            TempData.AddResult(result);

            return RedirectToAction("Index");
        }

        #endregion
    }
}