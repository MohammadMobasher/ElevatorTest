using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.ViewModels.Transportations.Car;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.Transportaions;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.Transportations.Controllers
{
    [Area("Transportations")]
    [ControllerRole("مدیریت تعرفه حمل و نقل")]
    public class ManageTransportationTariffController : BaseAdminController
    {
        private readonly CarTransportRepository _carTransportRepository;
        public ManageTransportationTariffController(CarTransportRepository carTransportRepository)
        {
            _carTransportRepository = carTransportRepository;
        }

        [ActionRole("لیست تمامی وسایل نقلیه")]
        public async Task<IActionResult> Index(CarTransportationSearchViewModel search)
        {
            var model = await _carTransportRepository
                .GetAllCars(search, this.CurrentPage, this.PageSize);

            this.PageCount = model.Item1;

            return View(model.Item2);
        }

        [ActionRole("ثبت وسیله نقلیه جدید")]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CarTransportationInsertViewModel model)
        {
            await _carTransportRepository.MapAddAsync(model);

            TempData.AddResult(SweetAlertExtenstion.Ok("اطلاعات با موفقیت ثبت شد"));

            return Redirect(IndexUrlWithQueryString);
        }

        [ActionRole("ویرایش وسیله نقلیه")]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _carTransportRepository.GetByIdAsync(id);

            if (model == null)
                return new BadRequestResult();

            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> Update(CarTransportaionUpdateViewModel model)
        //{
        //    var result = await _carTransportRepository.UpdateCars(model);

        //    TempData.AddResult(result);

        //    return Redirect(IndexUrlWithQueryString);
        //}

    }
}