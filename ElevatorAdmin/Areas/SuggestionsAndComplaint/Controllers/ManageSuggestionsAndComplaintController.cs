using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using DataLayer.ViewModels.SuggestionsAndComplaint;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.User;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.SuggestionsAndComplaint.Controllers
{
    [Area("SuggestionsAndComplaint")]
    [ControllerRole("مدیریت شکایات و پیشنهادات")]
    public class ManageSuggestionsAndComplaintController : BaseAdminController
    {
        private readonly SuggestionsAndComplaintRepository _suggestionsAndComplaintRepository;

        public ManageSuggestionsAndComplaintController(UsersAccessRepository usersAccessRepository,
            SuggestionsAndComplaintRepository suggestionsAndComplaintRepository) : base(usersAccessRepository)
        {
            _suggestionsAndComplaintRepository = suggestionsAndComplaintRepository;
        }

        [ActionRole("صفحه لیست شکایات و پیشنهادات")]
        public async Task<IActionResult> Index(SuggestionsAndComplaintSearchViewModel searchModel = null)
        {
            var model = await _suggestionsAndComplaintRepository.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);
        }

        public async Task<IActionResult> ShowItem(int Id)
        {
            var model = await _suggestionsAndComplaintRepository.GetByIdAsync(Id);
            return View(model);
        }
    }
}