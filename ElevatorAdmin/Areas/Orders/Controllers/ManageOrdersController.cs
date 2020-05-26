using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Core.CustomAttributes;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.BankRepository;
using Service.Repos.Product;
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
        private readonly ShopOrderDetailsRepository _shopOrderDetailsRepository;
        private readonly UserRepository _userRepository;
        private readonly UserAddressRepository _userAddressRepository;
        public ManageOrdersController(ShopOrderRepository shopOrderRepository
            , UsersPaymentRepository usersPaymentRepository
            , ShopProductRepository shopProductRepository
            , UsersAccessRepository usersAccessRepository
            , ShopOrderDetailsRepository  shopOrderDetailsRepository
            , UserRepository userRepository
            , UserAddressRepository userAddressRepository) : base(usersAccessRepository)
        {
            _shopOrderRepository = shopOrderRepository;
            _usersPaymentRepository = usersPaymentRepository;
            _shopProductRepository = shopProductRepository;
            _shopOrderDetailsRepository = shopOrderDetailsRepository;
            _userRepository = userRepository;
            _userAddressRepository = userAddressRepository;
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
        public async Task<IActionResult> OrderDetail(int id,int userId)
        {
            var model = _shopProductRepository.GetList(a => a.ShopOrderId == id  ,includes: "Product");

            var info = _shopOrderRepository.GetById(id);

            ViewBag.UserInfo =await _userRepository.GetByConditionAsync(a => a.Id == info.UserId);

            ViewBag.UserAddress = await _userAddressRepository.GetByConditionAsync(a => a.UserId == a.UserId);
            ViewBag.Order = info;



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