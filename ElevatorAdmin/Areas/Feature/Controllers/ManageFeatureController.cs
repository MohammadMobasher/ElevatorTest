﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities;
using DataLayer.ViewModels.Feature;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.User;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.Feature.Controllers
{
    [Area("Feature")]
    [AllowAnonymous()]
    public class ManageFeatureController : BaseAdminController
    {
        private readonly FeatureRepository _featureRepository;

        public ManageFeatureController(FeatureRepository featureRepository, UsersAccessRepository usersAccessRepository) : base(usersAccessRepository)
        {
            _featureRepository = featureRepository;
        }

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

        public async Task<IActionResult> Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(FeatureInsertViewModel model)
        {
            
            TempData.AddResult(await _featureRepository.Insert(model));

            return RedirectToAction("Index");
        }

        #endregion
    }
}