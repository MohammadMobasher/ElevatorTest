using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO;
using DataLayer.ViewModels;
using DataLayer.ViewModels.FaqGroup;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.FAQs;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.FaqGroup.Controllers
{
    [Area("FaqGroup")]
    [ControllerRole("مدیریت گروه‌های سوالات پرتکرار")]
    public class ManageFaqGroupController : BaseAdminController
    {
        private readonly FaqGroupRepository _faqGroupRepository;

        public ManageFaqGroupController(UsersAccessRepository usersAccessRepository,
            FaqGroupRepository faqGroupRepository) : base(usersAccessRepository)
        {
            _faqGroupRepository = faqGroupRepository;
        }

        [ActionRole("صفحه لیست  گروه‌های سوالات پرتکرار")]
        [HasAccess]
        public async Task<IActionResult> Index(FaqGroupSearchViewModel searchModel)
        {
            var model = await _faqGroupRepository.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);
        }


        #region ثبت
        [ActionRole("ثبت گروه جدید")]
        [HasAccess]
        public async Task<IActionResult> Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(FaqGroupInsertViewModel model)
        {
            TempData.AddResult(await _faqGroupRepository.AddAsync(model));

            return RedirectToAction("Index");
        }

        #endregion

        #region ویرایش
        [ActionRole("ویرایش یک گروه")]
        [HasAccess]
        public async Task<IActionResult> Update(int Id)
        {

            var result = await _faqGroupRepository.GetByIdAsync(Id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(FaqGroupUpdateViewModel model)
        {
            var result = await _faqGroupRepository.UpdateAsync(model);

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

            var result = await _faqGroupRepository.DeleteAsync(model.Id);
            TempData.AddResult(result);

            return RedirectToAction("Index");
        }

        #endregion
    }
}