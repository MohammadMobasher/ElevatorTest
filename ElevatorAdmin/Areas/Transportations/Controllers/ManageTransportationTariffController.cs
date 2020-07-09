using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.ViewModels.Transportations.Car;
using DataLayer.ViewModels.Transportations.Tariff;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.Transportaions;
using Service.Repos.User;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.Transportations.Controllers
{
    [Area("Transportations")]
    [ControllerRole("مدیریت تعرفه حمل و نقل")]
    public class ManageTransportationTariffController : BaseAdminController
    {
        private readonly TransportationTariffRepository _tariffRepository;
        public ManageTransportationTariffController(TransportationTariffRepository tariffRepository
            , UsersAccessRepository usersAccessRepository) : base(usersAccessRepository)
        {
            _tariffRepository = tariffRepository;
        }

        [ActionRole("لیست تمامی تعرفه های حمل و نقل")]
        public async Task<IActionResult> Index(TariffSearchViewModel search)
        {
            var model = await _tariffRepository
                .GetAllCars(search, this.CurrentPage, this.PageSize);

            this.PageCount = model.Item1;

            ViewBag.SearchModel = search;

            return View(model.Item2);
        }

        [ActionRole("ثبت تعرفه ی جدید")]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(TariffInsertViewModel model)
        {
            var result = await _tariffRepository.InsertTariff(model);

            TempData.AddResult(result);

            return Redirect(IndexUrlWithQueryString);
        }

        [ActionRole("ویرایش تعرفه")]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _tariffRepository.GetTariffById(id);

            if (model == null)
                return new BadRequestResult();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TariffUpdateViewModel model)
        {
            var result = await _tariffRepository.UpdateTariff(model);

            TempData.AddResult(result);


            return Redirect(IndexUrlWithQueryString);
        }

        [ActionRole("حذف تعرفه")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tariffRepository.DeleteTariff(id);

            TempData.AddResult(result);

            return Redirect(IndexUrlWithQueryString);
        }

       

    }
}