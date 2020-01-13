using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO;
using DataLayer.ViewModels;
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
        private readonly ProductFeatureRepository _productFeatureRepository;

        public ManageProductGroupFeatureController(UsersAccessRepository usersAccessRepository,
            ProductGroupRepository productGroupRepository,
            ProductGroupFeatureRepository productGroupFeatureRepository,
            ProductFeatureRepository productFeatureRepository) : base(usersAccessRepository)
        {
            _productGroupRepository = productGroupRepository;
            _productGroupFeatureRepository = productGroupFeatureRepository;
            _productFeatureRepository = productFeatureRepository;
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


        #region حذف

        [ActionRole("حذف واحد")]
        [HasAccess]
        public async Task<IActionResult> Delete(int Id, int GroupId)
        {
            ViewBag.GroupId = GroupId;

            var item = await _productGroupFeatureRepository.GetByIdAsync(Id);

            // تعداد گروه‌هایی که این ویژگی را دارند
            // برای نمایش به کاربر برای اطمینان از حدف این آیتم
            ViewBag.GroupNumHasFeature = await _productGroupFeatureRepository.NumberGroupHasFeature(item.FeatureId);

            // تعداد محصولاتی که این ویژگی را دارند
            // برای نمایش به کاربر برای اطمینان از حذف این آیتم
            ViewBag.ProductNumHasFeatuer = await _productFeatureRepository.NumberProductHasFeature(item.FeatureId);
            
            return View(new DeleteDTO { Id = Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {

            var result = await _productGroupFeatureRepository.DeleteAsync(model.Id);
            TempData.AddResult(result);

            return RedirectToAction("Index", new { Id = model.GroupId});
        }

        #endregion
    }
}