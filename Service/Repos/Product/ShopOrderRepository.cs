using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> CreateFactor(List<ShopProduct> list, int userId)
        {
            try
            {
                // در صورت داشتن مقدار فاکتوری ثبت نمی شود
                var _orderId = await CheckOrderFinaled(userId);

                if (_orderId == 0)
                {
                    var model = new ShopOrder()
                    {
                        Amount = await _shopProductRepository.CalculateCartPriceNumber(userId),
                        CreateDate = DateTime.Now,
                        IsSuccessed = false,
                        UserId = userId,

                    };

                    await AddAsync(model);
                    // مشخص کردن اینکه این سبد محصولات مربوط به کدام فاکتور می باشد
                    await _shopProductRepository.ChangeStatus(list, model.Id);
                    return model.Id;
                }
                else
                {
                    var model = GetByCondition(a => a.Id == _orderId);

                    model.Amount =await _shopProductRepository.CalculateCartPriceNumber(userId);

                    await UpdateAsync(model);
                }
                await _shopProductRepository.ChangeStatus(list, _orderId);
                return _orderId;

            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// این برای زمانی است که یک فاکتور زده شده ولی به تایید نایی نرسیده است 
        /// و دوباره میخواهد به سمت درگاه برود برای جلوگیری  از ازدیاد فاکتور ها
        /// </summary>
        /// <returns></returns>
        public async Task<int> CheckOrderFinaled(int userId)
        {
            var model = await GetListAsync(a => a.UserId == userId && !a.IsSuccessed);

            return model.Count() > 0 ? model.LastOrDefault().Id : 0;
        }

        /// <summary>
        /// زمانی که خرید با موفقیت انجام شد ما در دیتا بیس ثبت میکنیم
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> SuccessedOrder (int id, int userId)
        {
            var model = GetByCondition(a => a.Id == id && a.UserId == userId);

            if (model == null) return false;

            model.IsSuccessed = true;
            model.SuccessDate = DateTime.Now;

            await UpdateAsync(model);
            return true;
        }
    }
}
