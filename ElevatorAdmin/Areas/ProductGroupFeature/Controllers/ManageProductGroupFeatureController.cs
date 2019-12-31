using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.ViewModels.ProductGroupFeature;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.Product;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.ProductGroupFeature.Controllers
{

    [Area("ProductGroupFeature")]
    [ControllerRole("مدیریت ویژگی‌ها گروه‌ها")]
    public class ManageProductGroupFeatureController : BaseAdminController
    {

        private readonly ProductGroupRepository _productGroupRepository;
        private readonly ProductGroupFeatureRepository _productGroupFeatureRepository;

        public ManageProductGroupFeatureController(UsersAccessRepository usersAccessRepository,
            ProductGroupRepository productGroupRepository,
            ProductGroupFeatureRepository productGroupFeatureRepository) : base(usersAccessRepository)
        {
            _productGroupRepository = productGroupRepository;
            _productGroupFeatureRepository = productGroupFeatureRepository;
        }


        [ActionRole("صفحه ویژگی‌های گروه")]
        [HasAccess]
        public async Task<IActionResult> Index(int Id, ProductGroupFeatureSearchViewModel searchModel = null)
        {
            ViewBag.ProductGroup = await _productGroupRepository.GetByIdAsync(Id);

            var model = await _productGroupFeatureRepository.LoadAsyncCount(Id, this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);
        }


        #region ثبت


        [ActionRole("ثبت ویژگی جدید")]
        [HasAccess]
        public async Task<IActionResult> Insert(int ProductGroupId)
        {
            ViewBag.ProductGroupId = ProductGroupId;
            var model = await _productGroupFeatureRepository.GetOtherFeaturesByGroupId(ProductGroupId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ProductGroupFeatureInsertViewModel model)
        {

            TempData.AddResult(await _productGroupFeatureRepository.AddAsync(model));

            return RedirectToAction("Index", new { Id = model.ProductGroupId });
        }

        #endregion
    }
}