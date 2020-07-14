using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using DataLayer.ViewModels.WarehouseProductChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Repos;
using Service.Repos.User;
using Service.Repos.Warehouses;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class ManageWarehouseProductCheckController : BaseAdminController
    {

        private readonly WarehouseRepository _warehouseRepository;
        private readonly WarehouseProductCheckRepository _warehouseProductCheckRepository;
        private readonly ProductRepostitory _productRepostitory;

        public ManageWarehouseProductCheckController(WarehouseRepository warehouseRepository
            , WarehouseProductCheckRepository warehouseProductCheckRepository
            , UsersAccessRepository usersAccessRepository
            , ProductRepostitory productRepostitory) : base(usersAccessRepository)
        {
            _warehouseRepository = warehouseRepository;
            _warehouseProductCheckRepository = warehouseProductCheckRepository;
            _productRepostitory = productRepostitory;
        }

        [ActionRole("صفحه لیست ورودی‌ها و خروجی‌ها")]
        [HasAccess]
        public async Task<IActionResult> Index(int id, WarehouseProductCheckSearchViewModel searchModel)
        {
            
            var model = await _warehouseProductCheckRepository.LoadAsyncCount(
                id,
                this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;
            ViewBag.WarehouseId = id;
            return View(model.Item2);

            //var model = await _warehouseProductCheckRepository.GetAllTransactionInWarehouse(id);

            //ViewBag.WarehouseId = id;

            //return View(model);
        }

        [ActionRole("ثبت ورودی و خروجی")]
        [HasAccess]
        public async Task<IActionResult> Insert(int id)
        {
            ViewBag.Warehouse = id;

            ViewBag.Products = await _productRepostitory.TableNoTracking
                .OrderByDescending(a => a.Title)
                .ToListAsync();

            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Insert(WarehouseProductCheckInsertViewModel vm)
        {

            await _warehouseProductCheckRepository.MapAddAsync(vm);

            return RedirectToAction(nameof(Index),new { id = vm.WarehouseId});
        }

       
    }
}