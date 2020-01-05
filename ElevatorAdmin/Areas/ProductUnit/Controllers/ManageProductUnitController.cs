using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO;
using DataLayer.DTO.ProductUnit;
using DataLayer.ViewModels;
using DataLayer.ViewModels.ProductUnit;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.Product;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.ProductUnit.Controllers
{
    [Area("ProductUnit")]
    [ControllerRole("مدیریت واحد کالا‌ها")]
    public class ManageProductUnitController : BaseAdminController
    {
        private readonly ProductUnitRepository _productUnitRepository;
        public ManageProductUnitController(UsersAccessRepository usersAccessRepository,
            ProductUnitRepository productUnitRepository) : base(usersAccessRepository)
        {
            _productUnitRepository = productUnitRepository;
        }

        [ActionRole("صفحه لیست واحد‌ها")]
        [HasAccess]
        public async Task<IActionResult> Index(ProductUnitSearchViewModel searchModel)
        {

            var model = await _productUnitRepository.LoadAsyncCount(this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);
        }



        #region ثبت


        [ActionRole("ثبت واحد جدید")]
        [HasAccess]
        public async Task<IActionResult> Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ProductUnitInsertViewModel model)
        {
            TempData.AddResult(await _productUnitRepository.AddAsync(model));

            return RedirectToAction("Index");
        }

        #endregion


        #region ویرایش


        [ActionRole("ویرایش واحد")]
        [HasAccess]
        public async Task<IActionResult> Update(int Id)
        {
            var result = await _productUnitRepository.GetByIdAsync(Id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUnitUpdateViewModel model)
        {
            TempData.AddResult(await _productUnitRepository.UpdateAsync(model));

            return RedirectToAction("Index");
        }

        #endregion


        #region حذف

        [ActionRole("حذف واحد")]
        [HasAccess]
        public async Task<IActionResult> Delete(int Id)
        {

            return View(new DeleteDTO { Id = Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {

            var result = await _productUnitRepository.DeleteAsync(model.Id);
            TempData.AddResult(result);

            return RedirectToAction("Index");
        }

        #endregion
    }
}