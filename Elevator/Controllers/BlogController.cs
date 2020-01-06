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

namespace Elevator.Controllers
{
    
    public class BlogController : BaseController
    {

        private readonly NewsRepository _newsRepository;

        public BlogController(NewsRepository newsRepository) : base()
        {
            _newsRepository = newsRepository;
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

            this.TotalNumber = result.Item1;
            return View(result.Item2);
        }



        public async Task<IActionResult> NewsDetail(int Id)
        {
            return View(await _newsRepository.GetItemDetailAsync(Id));
        }
    }
}