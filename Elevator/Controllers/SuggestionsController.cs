using Core.CustomAttributes;
using Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.ViewModels.SuggestionsAndComplaint;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;

namespace Elevator.Controllers
{

    public class SuggestionsController : BaseController
    {
        private readonly SuggestionsAndComplaintRepository _suggestionsAndComplaintRepository;

        public SuggestionsController(SuggestionsAndComplaintRepository suggestionsAndComplaintRepository)
        {
            _suggestionsAndComplaintRepository = suggestionsAndComplaintRepository;
        }

        public async Task<IActionResult> Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(SuggestionsAndComplaintInsertViewModel vm)
        {
            TempData.AddResult(await _suggestionsAndComplaintRepository.InsertAsync(vm));
            return RedirectToAction("Insert");
        }
    }
}