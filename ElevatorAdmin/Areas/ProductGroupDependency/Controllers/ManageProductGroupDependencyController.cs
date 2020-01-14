using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
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

        public ManageProductGroupDependencyController(UsersAccessRepository usersAccessRepository,
            ProductGroupDependenciesRepository productGroupDependenciesRepository,
            ProductGroupRepository productGroupRepository,
            FeatureRepository featureRepository
            //,ConditionRepository conditionRepository
            ) : base(usersAccessRepository)
        {
            _productGroupDependenciesRepository = productGroupDependenciesRepository;
            _productGroupRepository = productGroupRepository;
            _featureRepository = featureRepository;
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


            ViewBag.ProductGroups = await _productGroupRepository.GetAll();
            ViewBag.Products = await _featureRepository.GetAll();

            return View(model.Item2);
        }



        #region ثبت

        [ActionRole("ثبت وابستگی جدید")]
        [HasAccess]
        public async Task<IActionResult> Insert()
        {
            ViewBag.ProductGroups = await _productGroupRepository.GetAll();
            ViewBag.Products = await _featureRepository.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ProductGroupDependenciesInsertViewModel model)
        {

            TempData.AddResult(await _productGroupDependenciesRepository.InsertAsync(model));
            return RedirectToAction("Index");
        }

        #endregion


    }
}