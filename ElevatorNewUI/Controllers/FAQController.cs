using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.DTO.FAQs;
using Elevator.Controllers;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.FAQs;

namespace ElevatorNewUI.Controllers
{
    public class FAQController : BaseController
    {
        private readonly FaqGroupRepository _faqGroupRepository;
        private readonly FAQRepository _fAQRepository;

        public FAQController(FaqGroupRepository faqGroupRepository,
            FAQRepository fAQRepository)
        {
            _faqGroupRepository = faqGroupRepository;
            _fAQRepository = fAQRepository;
        }

        public IActionResult Index()
        {
            var model = _faqGroupRepository.GetAllMap<FaqGroupDTO>();
            return View(model);
        }



        public async Task<IActionResult> Questions (int id)
        {
            List<FAQDTO> model = await _fAQRepository.GetItemsByGroupId(id);
            ViewBag.GroupTitle = _faqGroupRepository.GetById(id).Title;
            return View(model);
        }



        public async Task<IActionResult> Search(string text)
        {
            var model = await _fAQRepository.Search(text);
            return View(model);
        }

    }
}