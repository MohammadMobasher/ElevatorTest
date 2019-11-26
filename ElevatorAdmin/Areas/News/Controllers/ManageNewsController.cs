using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.DTO;
using DataLayer.ViewModels.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.News.Controllers
{
    [Area("News")]
    [AllowAnonymous()]
    public class ManageNewsController : BaseAdminController
    {

        private readonly NewsRepository _newsRepository;
        private readonly NewsGroupRepository _newsGroupRepository;

        public ManageNewsController(NewsRepository newsRepository,
            NewsGroupRepository newsGroupRepository)
        {
            _newsRepository = newsRepository;
            _newsGroupRepository = newsGroupRepository;


        }

        public async Task<IActionResult> Index()
        {

            var model = await _newsRepository.LoadAsync<NewsDTO>(this.CurrentPage, this.PageSize);

            return View(model);
        }


        #region ثبت

        public async Task<IActionResult> Insert()
        {
            var NewsGroups = await _newsGroupRepository.LoadAsync<NewsGroupDTO>();

            return View(NewsGroups);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(InsertNewsListView model, IFormFile ImageAddress)
        {

            return RedirectToAction("Index");
        }

        #endregion
    }
}