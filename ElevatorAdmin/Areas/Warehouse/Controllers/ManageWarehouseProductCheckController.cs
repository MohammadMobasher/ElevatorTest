using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.User;
using Service.Repos.Warehouses;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class ManageWarehouseProductCheckController : BaseAdminController
    {

        private readonly WarehouseRepository _warehouseRepository;
        private readonly WarehouseProductCheckRepository _warehouseProductCheckRepository;

        public ManageWarehouseProductCheckController(WarehouseRepository warehouseRepository
            , WarehouseProductCheckRepository warehouseProductCheckRepository
            , UsersAccessRepository usersAccessRepository) : base(usersAccessRepository)
        {
            _warehouseRepository = warehouseRepository;
            _warehouseProductCheckRepository = warehouseProductCheckRepository;
        }

        public async Task<IActionResult> Index(int id)
        {
            //var model = await 

            return View();
        }


    }
}