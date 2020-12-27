using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities;
using DataLayer.SSOT;
using DataLayer.ViewModels;
using DataLayer.ViewModels.ShopOrder;
using DataLayer.ViewModels.User;
using Elevator.Controllers;
using ElevatorNewUI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.Product;
using Service.Repos.TreeInfo;
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
        private readonly TreeRepository _treeRepository;
        private readonly ProductRepostitory _productRepostitory;
        private readonly ShopOrderPaymentRepository _shopOrderPaymentRepository;

        public ProfileController(UserRepository userRepository
            , ShopOrderRepository shopOrderRepository
            , ShopProductRepository shopProductRepository
            , UserAddressRepository userAddressRepository
            , ProductUnitRepository productUnitRepository
            , TreeRepository treeRepository
            , ProductRepostitory productRepostitory
            , ShopOrderPaymentRepository shopOrderPaymentRepository)
        {
            _userRepository = userRepository;
            _shopOrderRepository = shopOrderRepository;
            _shopProductRepository = shopProductRepository;
            _userAddressRepository = userAddressRepository;
            _productUnitRepository = productUnitRepository;
            _treeRepository = treeRepository;
            _productRepostitory = productRepostitory;
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
            //ViewBag.UserAddress = await _userAddressRepository.GetListAsync(x => x.UserId == UserId);


            var orders = await _shopOrderRepository
                .GetListAsync(a => a.UserId == UserId && !a.IsDeleted && !a.IsInvoice, o => o.OrderByDescending(a => a.SuccessDate));


            return View(orders.ToList());
        }

        public async Task<IActionResult> OrderDetail(int id)
        {
            var reloadPrice = await _shopOrderRepository.ReloadPrice(id);

            if (!reloadPrice.Succeed)
            {
                TempData.AddResult(reloadPrice);

                return RedirectToAction(nameof(Orders));
            }

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


        #region SpecialInvoice

        public async Task<IActionResult> ListSpecialInvoice(string Title)
        {
            ViewBag.Model = _userRepository.GetByCondition(a => a.Id == UserId);
            ViewBag.SidebarActive = ProfileSidebarSSOT.SpecialInvoice;
            var result = await _shopOrderRepository.ListSpecialInvoice(Title);
            ViewBag.Title = Title;

            return View(result);
        }


        public async Task<IActionResult> SpecialInvoiceDetail(int id)
        {
          
            //به روز رسانی قیمت کالا های داخل هر پیش فاکتور
            await _shopProductRepository.ProductsPriceCheck(id);

            var model = await _shopProductRepository.GetListAsync(a => a.ShopOrderId == id, null, "Product");

            var order = await _shopOrderRepository.GetByConditionAsync(a => a.Id == id && !a.IsDeleted);

            if (order == null || model == null)
                return NotFound();

            // اطلاعات کاربر
            ViewBag.UserInfo = await _userRepository.GetByConditionAsync(a => a.Id == order.UserId);
            ViewBag.Order = order;

            ViewBag.UserAddress = await _userAddressRepository.GetByConditionAsync(a => a.UserId == order.UserId);
            ViewBag.Model = _userRepository.GetByCondition(a => a.Id == UserId);
            ViewBag.SidebarActive = ProfileSidebarSSOT.SpecialInvoice;
            ViewBag.Unit = _productUnitRepository.GetList();
            ViewBag.Amount = order?.PaymentAmount;
            ViewBag.TransferAmount = order?.TransferProductPrice;

            return View(model);
        }




        #endregion

        #region لیست پیش‌فاکتور ها

        public async Task<IActionResult> ListInvoice(string Title)
        {
            ViewBag.Model = _userRepository.GetByCondition(a => a.Id == UserId);
            ViewBag.Title = Title;
            ViewBag.SidebarActive = ProfileSidebarSSOT.Invoice;

            var result = await _shopOrderRepository.ListInvoice(UserId, Title);

            return View(result);
        }


        public async Task<IActionResult> InvoiceDetail(int id)
        {
            //به روز رسانی قیمت کالا های داخل هر پیش فاکتور
            await _shopProductRepository.ProductsPriceCheck(id);

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



        public async Task<IActionResult> CreateFactorFromInvoce(int id)
        {
            var userId = this.UserId;

            var model = await _shopOrderRepository.OverWriteShopOrder(id, userId);

            return RedirectToAction("UserAddressFromInvoice", "ShopProduct", new { id = model });
        }

        #endregion


        #region حذف یک پیش فاکتور

        public async Task<IActionResult> InvoiceDelete(int id)
        {
            TempData.AddResult(await _shopOrderRepository.DeleteInvoice(id));

            return RedirectToAction("ListInvoice");
        }

        #endregion


        #region حذف یک پیش فاکتور

        public async Task<IActionResult> SpecialInvoiceDelete(int id)
        {
            TempData.AddResult(await _shopOrderRepository.DeleteInvoice(id));

            return RedirectToAction("ListSpecialInvoice");
        }

        #endregion

        #region حذف یک آیتم از یک پیش فاکتور

        public async Task<IActionResult> DeleteItemFromShopOrder(int shopOrderId, int productId, string urlBack)
        {

            #region حذف آیتم مورد نظر

            TempData.AddResult(await _shopProductRepository.DeleteItemFromInvoce(shopOrderId, productId));

            #endregion

            return RedirectToAction(urlBack, new { id = shopOrderId });
        }

        #endregion


        #region  به روز رسانی یک فاکتور و ...

        [HttpPost]
        public async Task<IActionResult> EditShopOrder(ShopOrderUpdateFromSite model, string urlBack)
        {

            TempData.AddResult(await _shopProductRepository.UpdateCountAllItems(model));

            return RedirectToAction(urlBack, new { id = model.ShopOrderId });
        }
        
        #endregion


        #region جستجو در بین کالاها

        public async Task<IActionResult> SearchProduct(string term)
        {
            var result = await _productRepostitory.SearchForSelect2(term);
                
            return Json(result);
        }

        #endregion

        #region اضافه کردن یک کالا به یک فاکتور

        public async Task<IActionResult> AddToShopOrder(int ProductId, int ShopOrderId, int Count, string urlBack)
        {
            var userId = this.GetUserId();
            TempData.AddResult(await _shopProductRepository.AddCart(ShopOrderId,ProductId, userId, Count));


            return RedirectToAction(urlBack, new { id = ShopOrderId });
        }

        #endregion

        public async Task<IActionResult> ListTree()
        {
            ViewBag.Model = _userRepository.GetByCondition(a => a.Id == UserId);
            ViewBag.SidebarActive = ProfileSidebarSSOT.Tree;
            var model = await _treeRepository.GetListAsync();
            return View(model);
        }

    }
}