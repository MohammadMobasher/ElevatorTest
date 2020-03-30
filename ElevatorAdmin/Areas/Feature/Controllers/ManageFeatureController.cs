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
using Newtonsoft.Json;
using Service.Repos;
using Service.Repos.Product;
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
        private readonly ProductGroupFeatureRepository _productGroupFeatureRepository;
        private readonly ProductFeatureRepository _productFeatureRepository;

        public ManageFeatureController(FeatureRepository featureRepository
            , FeatureItemRepository featureItemRepository
            , UsersAccessRepository usersAccessRepository,
            ProductGroupFeatureRepository productGroupFeatureRepository,
            ProductFeatureRepository productFeatureRepository) : base(usersAccessRepository)
        {
            _featureRepository = featureRepository;
            _featureItemRepository = featureItemRepository;
            _productGroupFeatureRepository = productGroupFeatureRepository;
            _productFeatureRepository = productFeatureRepository;
        }


        [ActionRole("صفحه لیست ویژگی‌ها")]
        [HasAccess]
        public async Task<IActionResult> Index(FeatureSearchViewModel searchModel)
        {

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
        public async Task<IActionResult> Update(int Id)
        {
            var result = await _featureRepository.GetByIdAsync(Id);

            //var getAllItem = await _featureItemRepository.GetAllFeatureItemByFeatureId(result.Id);
            //var lstFeature = new List<FeatureItemListShowViewModel>();

            //getAllItem.ForEach(a => lstFeature.Add(new FeatureItemListShowViewModel()
            //{
            //    feature = a.Value
            //}));
            //ViewBag.FeatureItems = JsonConvert.SerializeObject(lstFeature);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(FeatureUpdateViewModel model, FeatureItemsViewModel vm)
        {

            TempData.AddResult(await _featureRepository.UpdateAsync(model, vm));

            return RedirectToAction("Index");
        }

        #endregion

        #region Api
        [AllowAccess]
        public async Task<IActionResult> GetAllFeatureByFeatureId(int id)
        {
            var getAllItem = await _featureItemRepository.GetAllFeatureItemByFeatureId(id);

            var lstFeature = new List<FeatureItemListShowViewModel>();

            getAllItem.ForEach(a => lstFeature.Add(new FeatureItemListShowViewModel()
            {
                feature = a.Value
            }));

            return Json(lstFeature);
        }
        #endregion

        #region حذف

        [ActionRole("حذف ویژگی")]
        [HasAccess]
        public async Task<IActionResult> Delete(int Id)
        {
            // تعداد گروه‌هایی که این ویژگی را دارند
            // برای نمایش به کاربر برای اطمینان از حدف این آیتم
            ViewBag.GroupNumHasFeature = await _productGroupFeatureRepository.NumberGroupHasFeature(Id);

            // تعداد محصولاتی که این ویژگی را دارند
            // برای نمایش به کاربر برای اطمینان از حذف این آیتم
            ViewBag.ProductNumHasFeatuer = await _productFeatureRepository.NumberProductHasFeature(Id);

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

        #region نمایش در قسمت جستجو سایت

        [ActionRole("نمایش به عنوان جستجو")]
        public async Task<IActionResult> ShowInSearch(int Id)
        {
            TempData.AddResult(await _featureRepository.ShowInSearchSite(Id));

            return RedirectToAction("Index");
        }

        #endregion
    }
}