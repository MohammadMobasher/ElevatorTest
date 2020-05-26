using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Core.CustomAttributes;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.BankRepository;
using Service.Repos.User;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.Orders.Controllers
{
    [Area("Orders")]
    [ControllerRole("مدیریت سفارشات")]
    public class ManageOrdersController : BaseAdminController
    {
        private readonly ShopOrderRepository _shopOrderRepository;
        private readonly UsersPaymentRepository _usersPaymentRepository;
        private readonly ShopProductRepository _shopProductRepository;

        public ManageOrdersController(ShopOrderRepository shopOrderRepository
            , UsersPaymentRepository usersPaymentRepository
            , ShopProductRepository shopProductRepository
            , UsersAccessRepository usersAccessRepository) : base(usersAccessRepository)
        {
            _shopOrderRepository = shopOrderRepository;
            _usersPaymentRepository = usersPaymentRepository;
            _shopProductRepository = shopProductRepository;

        }

        [ActionRole("لیست سفارشات")]
        public async Task<IActionResult> Index(ShopOrdersSearchViewModel search)
        {
            var model = await _shopOrderRepository.OrderLoadAsync(search,this.CurrentPage,this.PageSize);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = search;
            return View(model.Item2);
        }

        [ActionRole("جزئیات سفارش")]
        public IActionResult OrderDetail(int id)
        {
            var model = _shopProductRepository.GetList(a => a.ShopOrderId == id && a.IsFinaly == true);

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