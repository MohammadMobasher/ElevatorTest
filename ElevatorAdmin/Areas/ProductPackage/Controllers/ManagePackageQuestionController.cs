﻿using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO.Feature;
using DataLayer.ViewModels;
using DataLayer.ViewModels.Feature;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.Package;
using Service.Repos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.ProductPackage.Controllers
{
    [Area("ProductPackage")]
    [ControllerRole("مدیریت سوالات پکیج")]
    public class ManagePackageQuestionController : BaseAdminController
    {

        private readonly PackageQuestionsRepository _packageQuestionsRepository;
        private readonly FeatureRepository _featureRepository;


        public ManagePackageQuestionController(UsersAccessRepository usersAccessRepository
            , PackageQuestionsRepository packageQuestionsRepository
            , FeatureRepository featureRepository) : base(usersAccessRepository)
        {
            _packageQuestionsRepository = packageQuestionsRepository;
            _featureRepository = featureRepository;
        }

        [ActionRole("لیست سوالات")]
        public async Task<IActionResult> Index()
        {
            var model = await _packageQuestionsRepository
                .GetListAsync<FeatureQuestionsViewModel>(a => a.IsQuestion == true);

            return View(model);
        }

        [ActionRole("ثبت سوال جدید")]
        public async Task<IActionResult> SubmitQuestion()
        {
            var listFeatures = await _featureRepository.GetListAsync<FeatureIdTitleDTO>();

            ViewBag.Features = listFeatures;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitQuestion(FeatureQuestionsViewModel vm)
        {
            TempData.AddResult(await _packageQuestionsRepository.SetQuestion(vm));

            return RedirectToAction("Index");
        }
    }
}
