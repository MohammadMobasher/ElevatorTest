using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.ViewModels.ProductGroup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.Product;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.ProductGroup.Controllers
{
    [Area("ProductGroup")]
    [ControllerRole("مدیریت گروه کالا‌ها")]
    public class ManageProductGroupController : BaseAdminController
    {

        private readonly ProductGroupRepository _productGroupRepository;

        public ManageProductGroupController(ProductGroupRepository productGroupRepository, UsersAccessRepository usersAccessRepository) : base(usersAccessRepository)
        {
            _productGroupRepository = productGroupRepository;
        }



        [ActionRole("صفحه لیست گروه کالاها")]
        [HasAccess]
        public async Task<IActionResult> Index(ProductGroupSearchViewModel searchModel)
        {
            var model = await _productGroupRepository.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);
        }



        #region ثبت
        [ActionRole("ثبت گروه کالا جدید")]
        [HasAccess]
        public async Task<IActionResult> Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ProductGroupInsertViewModel model)
        {
            var result = await _productGroupRepository.AddAsync(model);

            TempData.AddResult(result);

            return RedirectToAction("Index");
        }

        #endregion

        #region ویرایش
        [ActionRole("ویرایش گروه کالا")]
        [HasAccess]
        public async Task<IActionResult> Update(int Id)
        {

            var result = await _productGroupRepository.GetByIdAsync(Id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductGroupUpdateViewModel model)
        {
            var result = await _productGroupRepository.UpdateAsync(model);

            TempData.AddResult(result);

            return RedirectToAction("Index");
        }

        #endregion

        #region حذف
        [ActionRole("حذف گروه کالا")]
        [HasAccess]
        public async Task<IActionResult> Delete(int Id)
        {

            var result = await _productGroupRepository.DeleteAsync(Id);
            TempData.AddResult(result);

            return RedirectToAction("Index");
        }

        #endregion

    }
}