using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Core.Utilities;
using DataLayer.DTO.WarehouseDTO;
using DataLayer.ViewModels.Warehouse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Repos.User;
using Service.Repos.Warehouses;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class ManageWarehouseController : BaseAdminController
    {
        private readonly WarehouseRepository _warehouseRepository;


        public ManageWarehouseController(WarehouseRepository warehouseRepository, UsersAccessRepository usersAccessRepository) : base(usersAccessRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<IActionResult> Index(WarehouseSearchViewModel vm)
        {
            var model = await _warehouseRepository.TableNoTracking
                .ProjectTo<WarehouseFullDTO>().ToListAsync();

            return View(model);
        }


        public IActionResult Insert()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(WarehouseCreateViewModel vm)
        {
            await _warehouseRepository.MapAddAsync(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            return Redirect(IndexUrlWithQueryString); 
        }

        public async Task<IActionResult> Update(int id)
        {
            var model = await _warehouseRepository.TableNoTracking.Where(a=>a.Id == id)
                .ProjectTo<WarehouseFullDTO>().FirstOrDefaultAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(WarehouseUpdateViewModel vm)
        {
            TempData.AddResult(await _warehouseRepository.UpdateMappingAsync(vm));

            return Redirect(IndexUrlWithQueryString);
        }


    }
}