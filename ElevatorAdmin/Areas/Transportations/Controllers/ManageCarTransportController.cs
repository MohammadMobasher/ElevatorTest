using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.ViewModels.Transportations.Car;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.Transportaions;
using Service.Repos.User;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.Transportations.Controllers
{
    [Area("Transportations")]
    [ControllerRole("مدیریت وسایل نقلیه")]
    public class ManageCarTransportController : BaseAdminController
    {
        private readonly CarTransportRepository _carTransportRepository;
        public ManageCarTransportController(CarTransportRepository carTransportRepository
            , UsersAccessRepository usersAccessRepository) : base(usersAccessRepository)
        {
            _carTransportRepository = carTransportRepository;
        }

        [ActionRole("لیست تمامی وسایل نقلیه")]
        public async Task<IActionResult> Index(CarTransportationSearchViewModel search)
        {
            var model = await _carTransportRepository
                .GetAllCars(search, this.CurrentPage, this.PageSize);

            this.PageCount = model.Item1;

            ViewBag.SearchModel = search;

            return View(model.Item2);
        }

        [ActionRole("ثبت وسیله نقلیه جدید")]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CarTransportationInsertViewModel model
            ,IFormFile pic)
        {
            var result= await _carTransportRepository.InsertCar(model,pic);

            TempData.AddResult(result);

            return Redirect(IndexUrlWithQueryString);
        }

        [ActionRole("ویرایش وسیله نقلیه")]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _carTransportRepository.GetCarById(id);

            if (model == null)
                return new BadRequestResult();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarTransportaionUpdateViewModel model,IFormFile pic)
        {
            var result = await _carTransportRepository.UpdateCars(model,pic);

            TempData.AddResult(result);

            return Redirect(IndexUrlWithQueryString);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _carTransportRepository.DeleteCar(id);

            TempData.AddResult(result);

            return Redirect(IndexUrlWithQueryString);
        }


        public async Task<IActionResult> ChageStatus(int id)
        {
            TempData.AddResult(await _carTransportRepository.ChangeStatus(id));

            return Redirect(IndexUrlWithQueryString);
        }
    }
}