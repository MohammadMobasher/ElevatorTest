using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities;
using DataLayer.ViewModels.ShopProduct;
using Microsoft.EntityFrameworkCore;

namespace Service.Repos
{
    public class ShopProductRepository : GenericRepository<ShopProduct>
    {
        public ShopProductRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsExist(int productId, int userId)
        => await GetByConditionAsync(a => a.ProductId == productId && a.UserId == userId) !=null;

        public async Task<bool> IsPackageExist(int packageId, int userId)
            => await GetByConditionAsync(a => a.PackageId == packageId && a.UserId == userId) != null;


        public async Task<SweetAlertExtenstion> AddCart(int productId,int userId)
        {
            if (await IsExist(productId, userId))
                return SweetAlertExtenstion.Error("این کالا قبلا ثبت شده است");

            MapAdd(new ShopProductAddViewModel()
            {
                ProductId = productId,
                UserId = userId
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

            if(model == null) return SweetAlertExtenstion.Error("اطلاعاتی با این شناسه یافت نشد");

            Delete(model);

            return SweetAlertExtenstion.Ok();
        }

    }

}
