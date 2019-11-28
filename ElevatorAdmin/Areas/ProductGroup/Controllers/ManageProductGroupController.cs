using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities;
using DataLayer.ViewModels.ProductGroup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.Product;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.ProductGroup.Controllers
{
    [Area("ProductGroup")]
    [AllowAnonymous()]
    public class ManageProductGroupController : BaseAdminController
    {

        private readonly ProductGroupRepository _productGroupRepository;

        public ManageProductGroupController(ProductGroupRepository productGroupRepository)
        {
            _productGroupRepository = productGroupRepository;
        }




        public async Task<IActionResult> Index(ProductGroupSearchViewModel searchModel)
        {
            this.PageSize = 10;
            var model = await _productGroupRepository.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);
        }



        #region ثبت

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

        public async Task<IActionResult> Delete(int Id)
        {

            var result = await _productGroupRepository.DeleteAsync(Id);
            TempData.AddResult(result);

            return RedirectToAction("Index");
        }

        #endregion

    }
}