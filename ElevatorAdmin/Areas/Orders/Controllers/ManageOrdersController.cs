using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.BankRepository;

namespace ElevatorAdmin.Areas.Orders.Controllers
{
    [Area("Orders")]
    [ControllerRole("مدیریت سفارشات")]
    public class ManageOrdersController : Controller
    {
        private readonly ShopOrderRepository _shopOrderRepository;
        private readonly UsersPaymentRepository _usersPaymentRepository;

        public ManageOrdersController(ShopOrderRepository shopOrderRepository,UsersPaymentRepository usersPaymentRepository)
        {
            _shopOrderRepository = shopOrderRepository;
            _usersPaymentRepository = usersPaymentRepository;
        }

        [ActionRole("لیست سفارشات")]
        public IActionResult Index()
        {
            var model = _shopOrderRepository.GetList();

            return View(model);
        }

        [ActionRole("لیست تمامی اقدامات مالی انجام شده کاربر")]
        public IActionResult UserLog()
        {
            var model = _usersPaymentRepository.GetList();

            return View(model);
        }
    }
}