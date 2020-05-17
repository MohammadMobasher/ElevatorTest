using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Core;
using DataLayer.ViewModels.News;
using Elevator.Controllers;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using DataLayer.DTO;

namespace ElevatorNewUI.Controllers
{
    public class BlogController : BaseController
    {
        public IConfiguration _configuration { get; }
        private readonly NewsRepository _newsRepository;
        private readonly NewsGroupRepository _newsGroupRepository;

        public BlogController(NewsRepository newsRepository,
            IConfiguration Configuration,
            NewsGroupRepository newsGroupRepository) : base()
        {
            _newsRepository = newsRepository;
            _configuration = Configuration;
            _newsGroupRepository = newsGroupRepository;
        }


        public async Task<IActionResult> Index(string searchKey = "")
        {
            //=========================================================================
            NewsSearchViewModel searchModel = new NewsSearchViewModel();
            searchModel.Title = searchKey;
            //=========================================================================

            var result = await _newsRepository.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                searchModel);

            var test = _configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
            ViewBag.Url = test.SiteConfig.UrlAddress;

            ViewBag.NewGroups = this._newsGroupRepository.Load<NewsGroupDTO>();

            this.TotalNumber = result.Item1;
            return View(result.Item2);
        }



        public async Task<IActionResult> NewsDetail(int Id)
        {
            var model = await _newsRepository.GetItemDetailAsync(Id);
            if (model == null)
                return NotFound();
            var test = _configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
            ViewBag.Url = test.SiteConfig.UrlAddress;
            return View(model);
        }
    }
}