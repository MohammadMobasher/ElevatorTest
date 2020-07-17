using Core.Utilities;
using Dapper;
using DataLayer.Entities;
using DataLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos
{
    public class ShopOrderRepository : GenericRepository<ShopOrder>
    {
        private readonly ShopProductRepository _shopProductRepository;
        private readonly IDbConnection _connection;
        public ShopOrderRepository(DatabaseContext dbContext, ShopProductRepository shopProductRepository
            , IDbConnection connection) : base(dbContext)
        {
            _shopProductRepository = shopProductRepository;
            _connection = connection;
        }

        public async Task<int> CreateFactor(List<ShopProduct> list, int userId)
        {
            try
            {
                var tariff = CalculateTariff(userId) ?? 0;

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
                        TransferProductPrice = tariff,
                    };

                    model.PaymentAmount = model.Amount + model.TransferProductPrice;

                    await AddAsync(model);
                    // مشخص کردن اینکه این سبد محصولات مربوط به کدام فاکتور می باشد
                    await _shopProductRepository.ChangeStatus(list, model.Id);
                    return model.Id;
                }
                else
                {
                    var model = GetByCondition(a => a.Id == _orderId);

                    model.Amount = await _shopProductRepository.CalculateCartPriceNumber(userId);
                    model.TransferProductPrice = tariff;
                    model.PaymentAmount = model.Amount + tariff;

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
        public async Task<bool> SuccessedOrder(int id, int userId)
        {
            var model = GetByCondition(a => a.Id == id && a.UserId == userId);

            if (model == null) return false;

            model.IsSuccessed = true;
            model.SuccessDate = DateTime.Now;

            await UpdateAsync(model);
            return true;
        }


        public async Task<Tuple<int, List<ShopOrder>>> OrderLoadAsync(ShopOrdersSearchViewModel vm, int page = 0, int pageSize = 10)
        {
            var model = TableNoTracking;

            model = model.WhereIf(vm.OrderId != null, a => a.OrderId == vm.OrderId.ToString());
            model = model.WhereIf(vm.Amount != null, a => a.Amount == vm.Amount);
            model = model.WhereIf(vm.Status != null, a => a.Status == vm.Status);
            model = model.WhereIf(vm.IsSuccessed != null, a => a.IsSuccessed == vm.IsSuccessed);

            var count = model.Count();

            model = model.Include(a => a.Users);

            return new Tuple<int, List<ShopOrder>>(count,await model.Skip((page-1) * pageSize).Take(pageSize).ToListAsync());
        }


        public async Task<ShopOrder> GetItemByIdWithUserAsync(int Id)
        {
            return await TableNoTracking.Include(x => x.Users).Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        public long? CalculateTariff(int userId)
        {
            var sqlQuery = $@"
                DECLARE @UserInfo TABLE (productId int,UserArea int, Area int)
                INSERT INTO @UserInfo(productId,UserArea, Area)
                select distinct ShopProduct.ProductId , UserAddress.TehranAreasFrom , Warehouse.Region
                from AspNetUsers
                	JOIN ShopProduct ON AspNetUsers.Id = ShopProduct.UserId
                	JOIN UserAddress ON AspNetUsers.Id = UserAddress.UserId
                	JOIN WarehouseProductCheck on ShopProduct.ProductId = WarehouseProductCheck.ProductId
                	JOIN Warehouse on WarehouseProductCheck.WarehouseId = Warehouse.Id
                where IsFinaly = 0 and AspNetUsers.Id = {userId}
                
                DECLARE @OrderDetail TABLE (ProductSize BIGINT,Area int)
                insert INTO @OrderDetail(ProductSize,Area)
                select SUM(ProductSize),WareHouse.Area 
                from @UserInfo as WareHouse
                	JOIN Product on WareHouse.ProductId = Product.Id
                Group By Area
                
                SELECT SUM(Maxtariff) AS Tariff
                FROM (
                	select MAX(tariff) as Maxtariff 
                	from TransportationTariff
                		JOIN @OrderDetail as Orders on TransportationTariff.TehranAreasFrom = Orders.Area
                	Where TehranAreasTO = (select Top 1 UserArea from @UserInfo)
                		AND Orders.ProductSize between  TransportationTariff.ProductSizeFrom
                		AND TransportationTariff.ProductSizeTo
                	group by tehranareasFrom,TehranAreasTO,ProductSize
                ) t";

            var tariff =  _connection.Query<long?>(sqlQuery).FirstOrDefault();

            return tariff;
        }

    }
}
