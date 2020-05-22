using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.SSOT;
using Elevator.Controllers;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.User;

namespace ElevatorNewUI.Controllers.Profile
{
    public class ProfileController : BaseController
    {
        private readonly UserRepository _userRepository;
        private readonly ShopOrderRepository _shopOrderRepository;

        public ProfileController(UserRepository userRepository, ShopOrderRepository shopOrderRepository)
        {
            _userRepository = userRepository;
            _shopOrderRepository = shopOrderRepository;
        }


        public async Task<IActionResult> Index()
        {
            var model = await _userRepository.GetByConditionAsync(a => a.Id == UserId);
            ViewBag.SidebarActive = ProfileSidebarSSOT.Profile;


            return View(model);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }




        public async Task<IActionResult> Orders()
        {
            ViewBag.Model = _userRepository.GetByCondition(a => a.Id == UserId);
            ViewBag.SidebarActive = ProfileSidebarSSOT.Orders;

            var orders = await _shopOrderRepository.GetListAsync(a => a.UserId == UserId);

            return View(orders.ToList());
        }

        public IActionResult Favorite()
        {
            ViewBag.Model = _userRepository.GetByCondition(a => a.Id == UserId);
            ViewBag.SidebarActive = ProfileSidebarSSOT.Favorite;

            return View();
        }

        public IActionResult Account()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Account(int id)
        {
            return View();
        }

    }
}