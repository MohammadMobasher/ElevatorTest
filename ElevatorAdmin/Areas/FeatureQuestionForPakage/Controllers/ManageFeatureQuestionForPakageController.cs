using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO;
using DataLayer.DTO.Feature;
using DataLayer.ViewModels;
using DataLayer.ViewModels.FeatureQuestionForPakage;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.Features;
using Service.Repos.Product;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.FeatureQuestionForPakage.Controllers
{
    [Area("FeatureQuestionForPakage")]
    [ControllerRole("مدیریت ویژگی‌های سوالات پکیج")]
    public class ManageFeatureQuestionForPakageController : BaseAdminController
    {
        private readonly FeatureQuestionForPakageRepository _featureQuestionForPakageRepository;
        private readonly ProductGroupRepository _productGroupRepository;
        private readonly FeatureRepository _featureRepository;

        public ManageFeatureQuestionForPakageController(
            UsersAccessRepository usersAccessRepository,
            FeatureQuestionForPakageRepository featureQuestionForPakageRepository,
            ProductGroupRepository productGroupRepository,
            FeatureRepository featureRepository
            ) : base(usersAccessRepository)
        {
            _featureQuestionForPakageRepository = featureQuestionForPakageRepository;
            _productGroupRepository = productGroupRepository;
            _featureRepository = featureRepository;
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
            ViewBag.Groups = await _productGroupRepository.GetParentsAsync();
            ViewBag.Features = _featureRepository.GetAllMap<FeatureIdTitleDTO>();

            return View(model.Item2);
        }


        #region ثبت


        [ActionRole("ثبت سوال جدید")]
        public async Task<IActionResult> Insert()
        {
            ViewBag.Groups = await _productGroupRepository.GetParentsAsync();
            ViewBag.Features = _featureRepository.GetAllMap<FeatureIdTitleDTO>();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(FeatureQuestionForPakageInsertViewModel model)
        {

            TempData.AddResult(await _featureQuestionForPakageRepository.Insert(model));
            return RedirectToAction("Index");
        }

        #endregion



        #region ویرایش


        [ActionRole("ویرایش سوال")]
        public async Task<IActionResult> Update(int Id)
        {
            var result = await _featureQuestionForPakageRepository.GetByIdAsync(Id);
            ViewBag.Groups = await _productGroupRepository.GetParentsAsync();
            ViewBag.Features = _featureRepository.GetAllMap<FeatureIdTitleDTO>();

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(FeatureQuestionForPakageUpdateViewModel model)
        {

            TempData.AddResult(await _featureQuestionForPakageRepository.UpdateAsync(model));

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

            var result = await _featureQuestionForPakageRepository.DeleteAsync(model.Id);
            TempData.AddResult(result);

            return RedirectToAction("Index");
        }

        #endregion


        #region واکشی سوالات

        /// <summary>
        /// گرفتن تمامی سوالات ثبت شده برای پکیج
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllQuestions()
        {
             var questions =  _featureQuestionForPakageRepository.ListQuestions();

            return PartialView(questions);
        }

        #endregion

    }
}