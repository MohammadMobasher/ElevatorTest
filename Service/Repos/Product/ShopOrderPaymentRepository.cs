using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos.Product
{
    public class ShopOrderPaymentRepository : GenericRepository<ShopOrderPayment>
    {
        private readonly ShopOrderRepository _shopOrderRepository;

        public ShopOrderPaymentRepository(DatabaseContext dbContext
            ,ShopOrderRepository shopOrderRepository) : base(dbContext)
        {
            _shopOrderRepository = shopOrderRepository;
        }


        //public async Task<long?> CreatePayment(int orderId)
        //{
        //    var model = await _shopOrderRepository.GetByIdAsync(orderId);

        //    var count = model.

        //}


        /// <summary>
        /// در این تابع مشخص می شود که آیا برای یک فاکتور تمام رکورد ها پرداخت شده است یا نه
        /// </summary>
        /// <param name="shopOrderId">شماره فاکتور</param>
        /// <returns></returns>
        public async Task<bool> AllIsPay(int shopOrderId)
        {
            var items = await DbContext.ShopOrderPayment.Where(x => x.ShopOrderId == shopOrderId && x.IsSuccess == false).ToListAsync();
            if (items != null && items.Count > 0)
                return true;

            return false;

        }

    }
}
