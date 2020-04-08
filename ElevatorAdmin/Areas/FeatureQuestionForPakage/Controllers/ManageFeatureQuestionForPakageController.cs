using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.ViewModels.FeatureQuestionForPakage;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.Features;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.FeatureQuestionForPakage.Controllers
{
    [Area("FeatureQuestionForPakage")]
    [ControllerRole("مدیریت ویژگی‌های سوالات پکیج")]
    public class ManageFeatureQuestionForPakageController : BaseAdminController
    {
        private readonly FeatureQuestionForPakageRepository _featureQuestionForPakageRepository;


        public ManageFeatureQuestionForPakageController(FeatureQuestionForPakageRepository featureQuestionForPakageRepository)
        {
            _featureQuestionForPakageRepository = featureQuestionForPakageRepository;
        }

        [ActionRole("صفحه لیست ویژگی‌ها")]
        [HasAccess]
        public async Task<IActionResult> Index(FeatureQuestionForPakageSearchViewModel searchModel)
        {

            var model = await _featureQuestionForPakageRepository.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);
        }


        #region ثبت


        [ActionRole("ثبت سوال جدید")]
        public async Task<IActionResult> Insert()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(FeatureQuestionForPakageInsertViewModel model)
        {

            TempData.AddResult(await _featureQuestionForPakageRepository.Insert(model));
            return RedirectToAction("Index");


            return RedirectToAction("Index");
        }

        #endregion

    }
}