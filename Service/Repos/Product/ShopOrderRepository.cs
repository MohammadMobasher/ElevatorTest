using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos
{
    public class ShopOrderRepository : GenericRepository<ShopOrder>
    {
        private readonly ShopProductRepository _shopProductRepository;

        public ShopOrderRepository(DatabaseContext dbContext, ShopProductRepository shopProductRepository) : base(dbContext)
        {
            _shopProductRepository = shopProductRepository;
        }

        public async Task<int> CreateFactor(List<ShopProduct> list, int userId )
        {
            try
            {
                var model = new ShopOrder()
                {
                    Amount = await _shopProductRepository.CalculateCartPriceNumber(userId),
                    CreateDate = DateTime.Now,
                    IsSuccessed = false,
                    UserId = userId,

                };

                await AddAsync(model);
                await _shopProductRepository.ChangeStatus(list, model.Id);

                return model.Id;

            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool ChangeStatus(int id,int userId)
        {
            var model = GetByCondition(a => a.Id == id && a.UserId == userId);

            if (model == null) return false;

            model.IsSuccessed = true;

            Update(model);

            return true;
        }
    }
}
