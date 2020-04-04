using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities;
using DataLayer.ViewModels.ShopProduct;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Service.Repos
{
    public class ShopProductRepository : GenericRepository<ShopProduct>
    {
        public ShopProductRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsExist(int productId, int userId)
        => await GetByConditionAsync(a => a.ProductId == productId && a.UserId == userId) != null;

        public async Task<bool> IsPackageExist(int packageId, int userId)
            => await GetByConditionAsync(a => a.PackageId == packageId && a.UserId == userId) != null;


        public async Task<SweetAlertExtenstion> AddCart(int productId, int userId,int count=1)
        {

            if (await IsExist(productId, userId))
                return SweetAlertExtenstion.Error("این کالا قبلا ثبت شده است");

            MapAdd(new ShopProductAddViewModel()
            {
                ProductId = productId,
                UserId = userId,
                Count= count
            });

            return SweetAlertExtenstion.Ok();
        }

        public async Task<SweetAlertExtenstion> AddPackageCart(int packageId, int userId)
        {
            if (await IsPackageExist(packageId, userId))
                return SweetAlertExtenstion.Error("این پکیج قبلا ثبت شده است");

            MapAdd(new ShopProductAddPackageViewModel()
            {
                PackageId = packageId,
                UserId = userId
            });

            return SweetAlertExtenstion.Ok();
        }

        public async Task<SweetAlertExtenstion> RemoveCart(int id)
        {
            var model = await GetByIdAsync(id);

            if (model == null) return SweetAlertExtenstion.Error("اطلاعاتی با این شناسه یافت نشد");

            Delete(model);

            return SweetAlertExtenstion.Ok();
        }


        public List<ShopProduct> ShopProductByUserId(int userId)
        {
            var model = TableNoTracking
                .Include(a => a.Product)
                .Where(a => a.UserId == userId && a.IsFinaly == false).ToList();

            return model;
        }

        /// <summary>
        /// فانکشنی که تعداد را تغییر و قیمت را مجدد محاسبه میکند
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isPlus"></param>
        /// <returns></returns>
        public async Task<string> CartCountFunction(int id, bool isPlus)
        {
            var model = await GetByConditionAsync(a => a.Id == id, "Product");
            if (model == null) return null;

            if (model.Count == 1 && !isPlus) return null;

            model.Count = isPlus ? model.Count + 1 : model.Count - 1;
            await UpdateAsync(model);

            return (model.Count * model.Product.PriceWithDiscount).ToPersianPrice();
        }


        /// <summary>
        /// فانکشنی که تعداد را تغییر و قیمت را مجدد محاسبه میکند
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isPlus"></param>
        /// <returns></returns>
        public async Task<string> CartCountFunction(int id, int count)
        {
            var model = await GetByConditionAsync(a => a.Id == id, "Product");
            if (model == null) return null;

            model.Count = count <= 0 ? 1 : count;
            await UpdateAsync(model);

            return (model.Count * model.Product.PriceWithDiscount).ToPersianPrice();
        }

        /// <summary>
        /// محاسبه قیمت سبد خرید
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> CalculateCartPrice(int userId)
        {
            var model = await GetListAsync(a => a.UserId == userId && !a.IsFinaly, null, "Product,ProductPackage");

            if (model == null) return null;

            var sum = default(long);

            foreach (var item in model)
            {
                if (item.ProductId != null)
                {
                    sum += item.Product.PriceWithDiscount * item.Count;
                }
                else if (item.PackageId != null)
                {
                    //TODO
                }
            }

            return sum.ToPersianPrice();
        }

    }

}
