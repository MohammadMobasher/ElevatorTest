using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO;
using DataLayer.ViewModels;
using DataLayer.ViewModels.ProductGroupDependencies;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.Product;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.ProductGroupDependency.Controllers
{
    [Area("ProductGroupDependency")]
    [ControllerRole("مدیریت وابستگی‌ها")]
    public class ManageProductGroupDependencyController : BaseAdminController
    {
        private readonly ProductGroupDependenciesRepository _productGroupDependenciesRepository;
        private readonly ProductGroupRepository _productGroupRepository;
        private readonly FeatureRepository _featureRepository;
        private readonly ConditionRepository _conditionRepository;
        private readonly ProductGroupFeatureRepository _productGroupFeatureRepository;
        private readonly FeatureItemRepository _featureItemRepository;

        public ManageProductGroupDependencyController(UsersAccessRepository usersAccessRepository,
            ProductGroupDependenciesRepository productGroupDependenciesRepository,
            ProductGroupRepository productGroupRepository,
            FeatureRepository featureRepository,
            ConditionRepository conditionRepository,
            ProductGroupFeatureRepository productGroupFeatureRepository,
            FeatureItemRepository featureItemRepository
            ) : base(usersAccessRepository)
        {
            _productGroupDependenciesRepository = productGroupDependenciesRepository;
            _productGroupRepository = productGroupRepository;
            _featureRepository = featureRepository;
            _conditionRepository = conditionRepository;
            _productGroupFeatureRepository = productGroupFeatureRepository;
            _featureItemRepository = featureItemRepository;
        }


        [ActionRole("صفحه لیست وابستگی‌ها")]
        [HasAccess]
        public async Task<IActionResult> Index(ProductGroupDependenciesSearchViewModel searchModel)
        {
            var model = await _productGroupDependenciesRepository.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;


            ViewBag.ProductGroups = await _productGroupRepository.GetAllAsync();
            ViewBag.Products = await _featureRepository.GetAllAsync();

            return View(model.Item2);
        }



        #region ثبت

        [ActionRole("ثبت وابستگی جدید")]
        [HasAccess]
        public async Task<IActionResult> Insert()
        {
            ViewBag.ProductGroups = await _productGroupRepository.GetAllAsync();
            ViewBag.Products = await _featureRepository.GetAllAsync();
            ViewBag.Conditions = await _conditionRepository.GetAllAsync();


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ProductGroupDependenciesInsertViewModel model)
        {

            TempData.AddResult(await _productGroupDependenciesRepository.InsertAsync(model));
            return RedirectToAction("Index");
        }

        #endregion


        #region ثبت

        [ActionRole("ویرایش وابستگی")]
        [HasAccess]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.ProductGroups = await _productGroupRepository.GetAllAsync();
            ViewBag.Products = await _featureRepository.GetAllAsync();
            ViewBag.Conditions = await _conditionRepository.GetAllAsync();

            var model = await _productGroupDependenciesRepository.GetByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductGroupDependenciesUpdateViewModel model)
        {

            TempData.AddResult(await _productGroupDependenciesRepository.UpdateAsync(model));
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

            var result = await _productGroupDependenciesRepository.DeleteAsync(model.Id);
            TempData.AddResult(result);
            return RedirectToAction("Index");
        }
        #endregion


        #region api
        [HttpPost]
        public async Task<IActionResult> getFeatureByGroupId(int groupId)
        {
            var result = await _productGroupFeatureRepository.GetFeaturesByGroupIdRecAsync(groupId);

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> getFeatureValues(int featureId)
        {
            var result = await _featureItemRepository.GetitemsByFeatureId(featureId);

            return Json(result);
        }

        #endregion
    }
}