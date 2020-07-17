using DataLayer.Entities;
using System;
using System.Collections.Generic;
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

    }
}
