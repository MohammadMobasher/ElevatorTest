using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities;
using DataLayer.ViewModels.Warehouse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Repos.Warehouses;

namespace ElevatorAdmin.Areas.Warehouse.Controllers
{
    public class ManageWarehouseController : Controller
    {
        private readonly WarehouseRepository _warehouseRepository;

        public ManageWarehouseController(WarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<IActionResult> Index(WarehouseSearchViewModel vm)
        {
            var model = await _warehouseRepository.TableNoTracking.ToListAsync();

            return View(model);
        }


        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WarehouseCreateViewModel vm)
        {
            await _warehouseRepository.MapAddAsync(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            return RedirectToAction("Index"); 
        }

        public async Task<IActionResult> Update(int id)
        {
            var model = await _warehouseRepository.GetByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(WarehouseUpdateViewModel vm)
        {
            TempData.AddResult(await _warehouseRepository.UpdateMappingAsync(vm));

            return RedirectToAction("Index");
        }


    }
}