using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.SSOT;
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
        private readonly ShopOrderStatusRepository _shopOrderStatusRepository;
        private readonly SmsRestClient _smsRestClient;
        private readonly ProductUnitRepository _productUnitRepository;
        private readonly ShopOrderPaymentRepository _shopOrderPaymentRepository;

        public ManageOrdersController(ShopOrderRepository shopOrderRepository
            , UsersPaymentRepository usersPaymentRepository
            , ShopProductRepository shopProductRepository
            , UsersAccessRepository usersAccessRepository
            , ShopOrderDetailsRepository  shopOrderDetailsRepository
            , UserRepository userRepository
            , UserAddressRepository userAddressRepository,
            ShopOrderStatusRepository shopOrderStatusRepository,
            SmsRestClient smsRestClient,
            ProductUnitRepository productUnitRepository,
            ShopOrderPaymentRepository shopOrderPaymentRepository) : base(usersAccessRepository)
        {
            _shopOrderRepository = shopOrderRepository;
            _usersPaymentRepository = usersPaymentRepository;
            _shopProductRepository = shopProductRepository;
            _shopOrderDetailsRepository = shopOrderDetailsRepository;
            _userRepository = userRepository;
            _userAddressRepository = userAddressRepository;
            _shopOrderStatusRepository = shopOrderStatusRepository;
            _smsRestClient = smsRestClient;
            _productUnitRepository = productUnitRepository;
            _shopOrderPaymentRepository = shopOrderPaymentRepository;
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

            // اطلاعات فاکتور
            var info = await _shopOrderRepository.GetByIdAsync(id);
            // اطلاعات کاربر
            ViewBag.UserInfo = await _userRepository.GetByConditionAsync(a => a.Id == info.UserId);
            // اطلاعات آدرس کاربر
            //ViewBag.UserAddress = await _userAddressRepository.GetByConditionAsync(a => a.UserId == a.UserId);
            ViewBag.Order = info;

            // روند نمایی وضعیت فاکتور
            ViewBag.shopOrderStatuses = await _shopOrderStatusRepository.GetItemsByOrderId(id);
            ViewBag.UserAddress = await _userAddressRepository.GetByConditionAsync(a => a.ShopOrderId == info.Id);
            ViewBag.Order = info;
            ViewBag.Unit = await _productUnitRepository.GetListAsync();
            return View(model);
        }


        [ActionRole("تغییر وضعیت فاکتور")]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            // در این قسمت چک میشود که آیا تمام رکوردهای مربوط به این فاکتور پرداهت شده است یا نه
            // در صورتی که رکوردی داشته باشد که پرداختی نداشته باشد
            // در این صورت نمیتواند مرحله عوض کند
            if (await _shopOrderPaymentRepository.AllIsPay(id))
            {
                var order = await _shopOrderRepository.GetItemByIdWithUserAsync(id);
                var result = await _shopOrderStatusRepository.SendNextStatus(id);
                TempData.AddResult(result.Item1);
                if (result.Item2 != ShopOrderStatusSSOT.Nothing)
                    SendSmsChangeStatus(result.Item2, order.Users.PhoneNumber, order.Id.ToString(), order.Users.FirstName + " " + order.Users.LastName);
                return Redirect(IndexUrlWithQueryString);
            }

            TempData.AddResult(SweetAlertExtenstion.Error("هنوز تمامی رکورد های مربوط به این فاکتور پرداخت نشده است"));
            return Redirect(IndexUrlWithQueryString);
        }


        private void SendSmsChangeStatus(ShopOrderStatusSSOT status, string phoneNumber, string orderId, string fullName)
        {
            
            int bodyId = -1;
            switch (status)
            {
                case ShopOrderStatusSSOT.Ordered:
                    bodyId = (int)SmsBaseCodeSSOT.Ordered;
                    break;
                case ShopOrderStatusSSOT.Preparation:
                    bodyId = (int)SmsBaseCodeSSOT.Preparation;
                    break;
                case ShopOrderStatusSSOT.Loading:
                    bodyId = (int)SmsBaseCodeSSOT.Loading;
                    break;
                case ShopOrderStatusSSOT.transmission:
                    bodyId = (int)SmsBaseCodeSSOT.transmission;
                    break;
                case ShopOrderStatusSSOT.Delivery:
                    bodyId = (int)SmsBaseCodeSSOT.Delivery;
                    break;
            }
            string text = $"{fullName};{orderId}";
            var result = _smsRestClient.SendByBaseNumber(text, phoneNumber, bodyId);
        }



        [ActionRole("لیست تمامی اقدامات مالی انجام شده کاربر")]
        public IActionResult UserLog()
        {
            var model = _usersPaymentRepository.GetList();

            return View(model); 
        }

        [ActionRole("حذف فاکتور")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var model = await _shopOrderRepository.DeleteOrder(id);

            TempData.AddResult(model);

            return Redirect(IndexUrlWithQueryString);
        }


        /// <summary>
        /// بعد از اینکه کاربر به صوریت حظوری فاکتور خود را پرداخت کرد از طریق این متد
        /// اطلاعات فاکتورش را بروز رسانی میکنیم
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SetOfflinePayment(int orderId)
        {
            var model = await _shopOrderPaymentRepository.SetOfflinePayment(orderId);

            TempData.AddResult(model);

            return Redirect(IndexUrlWithQueryString);
        }
    }
}