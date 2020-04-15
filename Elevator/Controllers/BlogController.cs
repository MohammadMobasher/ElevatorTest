using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities;
using DataLayer.DTO;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using AutoMapper;
using DataLayer.ViewModels.News;
using Core;

namespace Elevator.Controllers
{
    
    public class BlogController : BaseController
    {
        public IConfiguration _configuration { get; }
        private readonly NewsRepository _newsRepository;

        public BlogController(NewsRepository newsRepository,
            IConfiguration Configuration) : base()
        {
            _newsRepository = newsRepository;
            _configuration = Configuration;
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

            this.TotalNumber = result.Item1;
            return View(result.Item2);
        }



        public async Task<IActionResult> NewsDetail(int Id)
        {
            var test = _configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
            ViewBag.Url = test.SiteConfig.UrlAddress;
            return View(await _newsRepository.GetItemDetailAsync(Id));
        }
    }
}