using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elevator.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.User;

namespace ElevatorNewUI.Controllers.Product
{
    [Authorize]
    public class UserOrderController : BaseController
    {
        private readonly ShopOrderRepository _shopOrderRepository;
        private readonly ShopProductRepository _shopProductRepository;
        private readonly UserRepository _userRepository;
        private readonly UserAddressRepository _userAddressRepository;

        public UserOrderController(ShopOrderRepository shopOrderRepository
            , ShopProductRepository shopProductRepository
            , UserRepository userRepository
            , UserAddressRepository userAddressRepository)
        {
            _shopOrderRepository = shopOrderRepository;
            _shopProductRepository = shopProductRepository;
            _userRepository = userRepository;
            _userAddressRepository = userAddressRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _shopOrderRepository
                .GetListAsync(a => a.UserId == UserId && a.IsSuccessed);


            return View(model);
        }

        public async Task<IActionResult> Result(string orderId,int shopOrderId)
        {
            var userInfo = await _userRepository.GetByConditionAsync(a => a.Id == UserId);
            var model = await _shopOrderRepository.GetByConditionAsync(a => a.OrderId == orderId && a.UserId == UserId);
            ViewBag.Order = await _shopProductRepository.GetListAsync(a => a.ShopOrderId == shopOrderId && a.UserId == UserId,null, "Product,ProductPackage");

            ViewBag.PhoneNumber = userInfo.PhoneNumber;
            ViewBag.FullName = userInfo.FirstName + " " + userInfo.LastName;
            ViewBag.UserAddress = await _userAddressRepository.GetByConditionAsync(a => a.UserId == UserId);

            return View(model);
        }
    }
}