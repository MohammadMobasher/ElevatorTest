using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.SSOT;
using DataLayer.ViewModels.User;
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
        private readonly ShopProductRepository _shopProductRepository;
        private readonly UserAddressRepository _userAddressRepository;
        public ProfileController(UserRepository userRepository
            , ShopOrderRepository shopOrderRepository
            , ShopProductRepository shopProductRepository
            , UserAddressRepository userAddressRepository)
        {
            _userRepository = userRepository;
            _shopOrderRepository = shopOrderRepository;
            _shopProductRepository = shopProductRepository;
            _userAddressRepository = userAddressRepository;
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

            var orders = await _shopOrderRepository
                .GetListAsync(a => a.UserId == UserId && a.IsSuccessed, o=>o.OrderByDescending(a=>a.SuccessDate));

            return View(orders.ToList());
        }


        public async Task<IActionResult> OrderDetail(int id)
        {
            var model = await _shopProductRepository.GetListAsync(a => a.ShopOrderId == id && a.IsFinaly);

            ViewBag.Model = _userRepository.GetByCondition(a => a.Id == UserId);
            ViewBag.SidebarActive = ProfileSidebarSSOT.Orders;

            return View(model);
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


        #region Change Account Info 

        public async Task<IActionResult> EditProfile()
        {
            var model = await _userRepository.GetByConditionAsync(a => a.Id == this.UserId);
            ViewBag.SidebarActive = ProfileSidebarSSOT.EditProfile;
            ViewBag.UserAddress = await _userAddressRepository.GetByConditionAsync(a => a.UserId == UserId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel vm, EditProfileUserAddressViewModel model)
        {
            vm.Id = UserId;
            model.UserId = UserId;

            await _userRepository.MapUpdateAsync(vm,vm.Id);
            await _userAddressRepository.MapUpdateAsync(model, model.UserId);

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}