using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO;
using DataLayer.DTO.FAQs;
using DataLayer.ViewModels;
using DataLayer.ViewModels.FAQ;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.FAQs;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.FAQ.Controllers
{
    [Area("FAQ")]
    [ControllerRole("مدیریت سوالات پرتکرار")]
    public class ManageFAQController : BaseAdminController
    {
        private readonly FAQRepository _fAQRepository;
        private readonly FaqGroupRepository _faqGroupRepository;

        public ManageFAQController(
            UsersAccessRepository usersAccessRepository,
            FAQRepository fAQRepository,
            FaqGroupRepository faqGroupRepository) : base(usersAccessRepository)
        {
            _fAQRepository = fAQRepository;
            _faqGroupRepository = faqGroupRepository;
        }

        [ActionRole("صفحه لیست سوالات پرتکرار")]
        [HasAccess]
        public async Task<IActionResult> Index(FAQSearchViewModel searchModel)
        {
            ViewBag.FaqGroups = _faqGroupRepository.GetAllMap<FaqGroupDTO>();

            var model = await _fAQRepository.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);

        }



        #region ثبت
        [ActionRole("ثبت سوال جدید")]
        [HasAccess]
        public async Task<IActionResult> Insert()
        {
            var model = _faqGroupRepository.GetAllMap<FaqGroupDTO>();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(FAQInsertViewModel model)
        {
            TempData.AddResult(await _fAQRepository.AddAsync(model));

            return RedirectToAction("Index");
        }

        #endregion

        #region ویرایش
        [ActionRole("ویرایش سوال پرتکرار")]
        [HasAccess]
        public async Task<IActionResult> Update(int Id)
        {
            ViewBag.FaqGroups = _faqGroupRepository.GetAllMap<FaqGroupDTO>();
            var result = await _fAQRepository.GetByIdAsync(Id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(FAQUpdateViewModel model)
        {
            var result = await _fAQRepository.UpdateAsync(model);

            TempData.AddResult(result);

            return RedirectToAction("Index");
        }

        #endregion

        #region حذف

        [ActionRole("حذف آیتم")]
        [HasAccess]
        public async Task<IActionResult> Delete(int Id)
        {

            return View(new DeleteDTO { Id = Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {

            var result = await _fAQRepository.DeleteAsync(model.Id);
            TempData.AddResult(result);

            return RedirectToAction("Index");
        }

        #endregion

    }
}