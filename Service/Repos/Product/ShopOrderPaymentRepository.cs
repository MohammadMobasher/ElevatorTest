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
            , ShopOrderRepository shopOrderRepository) : base(dbContext)
        {
            _shopOrderRepository = shopOrderRepository;
        }


        public async Task<int> CreatePayment(int orderId)
        {
            var model = await _shopOrderRepository.GetByIdAsync(orderId);

            var count = Math.Ceiling((decimal)model.PaymentAmount / 50000000);

            var adds = new List<ShopOrderPayment>();

            if (count == 1)
            {
                adds.Add(new ShopOrderPayment()
                {
                    ShopOrderId = orderId,
                    IsSuccess = false,
                    PaymentAmount = model.PaymentAmount,
                    PaymentDate = null,
                });
            }
            else
            {
                var payment = model.PaymentAmount;
                for (int i = 0; i < count; i++)
                {
                    adds.Add(new ShopOrderPayment()
                    {
                        ShopOrderId = orderId,
                        IsSuccess = false,
                        PaymentAmount = payment > 50000000 ? 50000000 : payment,
                        PaymentDate = null,
                    });

                    payment = payment - 50000000;
                }
            }

            await AddRangeAsync(adds);

            return (int)count;
        }

    }
}
