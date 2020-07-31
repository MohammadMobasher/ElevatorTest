using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.SSOT;
using DataLayer.ViewModels.User;
using Elevator.Controllers;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.Product;
using Service.Repos.User;

namespace ElevatorNewUI.Controllers.Profile
{
    public class ProfileController : BaseController
    {
        private readonly UserRepository _userRepository;
        private readonly ShopOrderRepository _shopOrderRepository;
        private readonly ShopProductRepository _shopProductRepository;
        private readonly UserAddressRepository _userAddressRepository;
        private readonly ProductUnitRepository _productUnitRepository;
        private readonly ShopOrderPaymentRepository _shopOrderPaymentRepository;

        public ProfileController(UserRepository userRepository
            , ShopOrderRepository shopOrderRepository
            , ShopProductRepository shopProductRepository
            , UserAddressRepository userAddressRepository
            , ProductUnitRepository productUnitRepository
            , ShopOrderPaymentRepository shopOrderPaymentRepository)
        {
            _userRepository = userRepository;
            _shopOrderRepository = shopOrderRepository;
            _shopProductRepository = shopProductRepository;
            _userAddressRepository = userAddressRepository;
            _productUnitRepository = productUnitRepository;
            _shopOrderPaymentRepository = shopOrderPaymentRepository;
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
            ViewBag.UserAddress = await _userAddressRepository.GetListAsync(x => x.UserId == UserId);


            var orders = await _shopOrderRepository
                .GetListAsync(a => a.UserId == UserId && !a.IsDeleted && !a.IsInvoice, o => o.OrderByDescending(a => a.SuccessDate));


            return View(orders.ToList());
        }


        public async Task<IActionResult> OrderDetail(int id)
        {
            var model = await _shopProductRepository.GetListAsync(a => a.ShopOrderId == id && a.IsFactorSubmited, null, "Product");

            var order = await _shopOrderRepository.GetByConditionAsync(a => a.Id == id && !a.IsDeleted);

            if (order == null || model == null)
                return NotFound();

            // لیست تراکنش های کاربر برای این فاکتور
            var payment = await _shopOrderPaymentRepository.GetListAsync(a => a.ShopOrderId == id);

            // اطلاعات کاربر
            ViewBag.UserInfo = await _userRepository.GetByConditionAsync(a => a.Id == order.UserId);
            ViewBag.Order = order;
            ViewBag.payment = payment;
            ViewBag.UserAddress = await _userAddressRepository.GetByConditionAsync(a => a.UserId == order.UserId);
            ViewBag.Model = _userRepository.GetByCondition(a => a.Id == UserId);
            ViewBag.SidebarActive = ProfileSidebarSSOT.Orders;
            ViewBag.Unit = _productUnitRepository.GetList();
            ViewBag.Amount = order?.PaymentAmount;
            ViewBag.TransferAmount = order?.TransferProductPrice;

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

            await _userRepository.MapUpdateAsync(vm, vm.Id);
            await _userAddressRepository.MapUpdateAsync(model, model.UserId);

            return RedirectToAction(nameof(Index));
        }

        #endregion


        #region لیست پیش‌فاکتور ها

        public async Task<IActionResult> ListInvoice()
        {
            ViewBag.Model = _userRepository.GetByCondition(a => a.Id == UserId);
            ViewBag.SidebarActive = ProfileSidebarSSOT.Invoice;
            var result = await _shopOrderRepository.ListInvoice(UserId);

            return View(result);
        }


        public async Task<IActionResult> InvoiceDetail(int id)
        {
            var model = await _shopProductRepository.GetListAsync(a => a.ShopOrderId == id, null, "Product");

            var order = await _shopOrderRepository.GetByConditionAsync(a => a.Id == id && !a.IsDeleted);

            if (order == null || model == null)
                return NotFound();

            // اطلاعات کاربر
            ViewBag.UserInfo = await _userRepository.GetByConditionAsync(a => a.Id == order.UserId);
            ViewBag.Order = order;

            ViewBag.UserAddress = await _userAddressRepository.GetByConditionAsync(a => a.UserId == order.UserId);
            ViewBag.Model = _userRepository.GetByCondition(a => a.Id == UserId);
            ViewBag.SidebarActive = ProfileSidebarSSOT.Invoice;
            ViewBag.Unit = _productUnitRepository.GetList();
            ViewBag.Amount = order?.PaymentAmount;
            ViewBag.TransferAmount = order?.TransferProductPrice;

            return View(model);
        }



        public async Task<IActionResult> CreateFactor(int id)
        {
            var model = await _shopOrderRepository.GetByIdAsync(id);



            return View();
        }

        #endregion

    }
}