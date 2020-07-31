﻿using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities;
using DataLayer.ViewModels.ShopProduct;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Service.Repos.Warehouses;

namespace Service.Repos
{
    public class ShopProductRepository : GenericRepository<ShopProduct>
    {
        private readonly WarehouseProductCheckRepository _warehouseProductCheckRepository;

        public ShopProductRepository(
             DatabaseContext dbContext
            , WarehouseProductCheckRepository warehouseProductCheckRepository) : base(dbContext)
        {
            _warehouseProductCheckRepository = warehouseProductCheckRepository;
        }

        public async Task<bool> IsExist(int productId, int userId)
        => await GetByConditionAsync(a => a.ProductId == productId && a.UserId == userId && !a.IsFinaly) != null;

        public async Task<bool> IsPackageExist(int packageId, int userId)
            => await GetByConditionAsync(a => a.PackageId == packageId && a.UserId == userId) != null;


        public async Task<SweetAlertExtenstion> AddCart(int productId, int userId, int count = 1)
        {

            var model = await GetByConditionAsync(a => a.ProductId == productId && a.UserId == userId && !a.IsFinaly && !a.IsFactorSubmited);

            if (model != null)
            {
                model.Count = count;

                await UpdateAsync(model);
                return SweetAlertExtenstion.Error("این محصول قبلا به سبد خرید اضافه شده است");
            }

            MapAdd(new ShopProductAddViewModel()
            {
                ProductId = productId,
                UserId = userId,
                Count = count,

            });

            return SweetAlertExtenstion.Ok();
        }

        public async Task<SweetAlertExtenstion> AddPackageCart(int packageId, int userId, int count = 1)
        {
            if (await IsPackageExist(packageId, userId))
                return SweetAlertExtenstion.Error("این پکیج قبلا ثبت شده است");

            MapAdd(new ShopProductAddPackageViewModel()
            {
                PackageId = packageId,
                UserId = userId,
                Count = count
            });

            return SweetAlertExtenstion.Ok();
        }

        public async Task<SweetAlertExtenstion> RemoveCart(int id)
        {
            try
            {
                var model = await GetByIdAsync(id);
                var w = new DataLayer.Entities.Warehouse.WarehouseProductCheck
                {
                    Count = model.Count,
                    Date = DateTime.Now,
                    ProductId = model.ProductId.Value,
                    TypeSSOt = DataLayer.SSOT.WarehouseTypeSSOT.In,
                };


                if (model == null) return SweetAlertExtenstion.Error("اطلاعاتی با این شناسه یافت نشد");

                Delete(model);

                await _warehouseProductCheckRepository.AddFromShopOrder(w);

                return SweetAlertExtenstion.Ok();
            }
            catch
            {
                return SweetAlertExtenstion.Error();
            }
        }


        public List<ShopProduct> ShopProductByUserId(int userId)
        {
            var model = TableNoTracking
                .Include(a => a.Product)
                .Include(a => a.ProductPackage)
                .Where(a => a.UserId == userId && a.IsFinaly == false && !a.IsFactorSubmited).ToList();

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
        public async Task<string> CartPackageCountFunction(int id, bool isPlus)
        {
            var model = await GetByConditionAsync(a => a.Id == id, "ProductPackage");
            if (model == null) return null;

            if (model.Count == 1 && !isPlus) return null;

            model.Count = isPlus ? model.Count + 1 : model.Count - 1;
            await UpdateAsync(model);

            var sum = default(string);

            if (model.ProductPackage.PackageWithDiscounts != null)
            {
                sum = (model.Count * model.ProductPackage.PackageWithDiscounts.Value).ToPersianPrice();
            }
            else
            {
                sum = (model.Count * model.ProductPackage.PackagePrice).ToPersianPrice();
            }

            return sum;
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
        /// فانکشنی که تعداد را تغییر و قیمت را مجدد محاسبه میکند
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isPlus"></param>
        /// <returns></returns>
        public async Task<string> CartPackageCountFunction(int id, int count)
        {
            var model = await GetByConditionAsync(a => a.Id == id, "ProductPackage");
            if (model == null) return null;

            model.Count = count <= 0 ? 1 : count;
            await UpdateAsync(model);


            var sum = default(string);

            if (model.ProductPackage.PackageWithDiscounts != null)
            {
                sum = (model.Count * model.ProductPackage.PackageWithDiscounts.Value).ToPersianPrice();
            }
            else
            {
                sum = (model.Count * model.ProductPackage.PackagePrice).ToPersianPrice();
            }

            return sum;
        }



        /// <summary>
        /// محاسبه قیمت سبد خرید
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> CalculateCartPrice(int userId)
        {
            var model = await GetListAsync(a => a.UserId == userId && !a.IsFinaly && !a.IsFactorSubmited, null, "Product,ProductPackage");

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
                    if (item.ProductPackage.PackageWithDiscounts == null)
                    {
                        sum += item.ProductPackage.PackageWithDiscounts.Value * item.Count;
                    }
                    else
                    {
                        sum += item.ProductPackage.PackagePrice * item.Count;
                    }

                }
            }

            return sum.ToPersianPrice();
        }





        /// <summary>
        /// محاسبه قیمت سبد خرید بر اساس شناسه کاربر و محاسبه مستقیم از سبد خرید به فاکتور
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<long> CalculateCartPriceNumber(int userId)
        {
            var model = await GetListAsync(a => a.UserId == userId && !a.IsFinaly && !a.IsFactorSubmited, null, "Product,ProductPackage");

            if (model == null) return 0;

            var sum = default(long);

            foreach (var item in model)
            {
                if (item.ProductId != null)
                {
                    sum += item.Product.PriceWithDiscount * item.Count;
                }
                else if (item.PackageId != null)
                {
                    if (item.ProductPackage.PackageWithDiscounts == null)
                    {
                        sum += item.ProductPackage.PackageWithDiscounts.Value * item.Count;
                    }
                    else
                    {
                        sum += item.ProductPackage.PackagePrice * item.Count;
                    }

                }
            }

            return sum;
        }


        /// <summary>
        /// محاسبه قیمت سبد خرید بر اساس شناسه کاربر و محاسبه مستقیم از سبد خرید به فاکتور
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<long> CalculateCartPriceFromInvice(int orderId)
        {
            var model = await GetListAsync(a => a.ShopOrderId == orderId, null, "Product");

            if (model == null) return 0;

            var sum = default(long);

            foreach (var item in model)
            {
                if (item.ProductId != null)
                {
                    sum += item.Product.PriceWithDiscount * item.Count;
                }

            }

            return sum;
        }

        /// <summary>
        /// تغییر وضعیت سبد خرید 
        /// مشخص کردن فاکتور
        /// </summary>
        /// <param name="list"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<bool> ChangeStatus(List<ShopProduct> list, int orderId)
        {
            list.ForEach(a => { a.ShopOrderId = orderId; a.IsFactorSubmited = true; });

            await UpdateRangeAsync(list, false);

            return Save();
        }

        /// <summary>
        /// زمانی که خرید با موفقیت انجام شد ما در دیتا بیس مشخص میکنیم
        /// و دیگر محصولات ثبت شده را نمایش نمی دهیم
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> SuccessedOrder(int orderId, int userId)
        {
            var model = await GetListAsync(a => a.ShopOrderId == orderId && a.UserId == userId, null, "Product,ProductPackage");

            if (model == null) return false;

            foreach (var item in model)
            {
                item.IsFinaly = true;

                if (item.ProductId != null)
                {
                    item.OrderName = item.Product.Title;
                    item.OrderPrice = item.Product.Price.ToPersianPrice();
                    item.OrderPriceDiscount = item.Product.PriceWithDiscount.ToPersianPrice();
                }
                else
                {
                    item.OrderName = item.ProductPackage.Title;
                    item.OrderPrice = item.ProductPackage.PackagePrice.ToPersianPrice();
                    item.OrderPriceDiscount = item.ProductPackage.PackageWithDiscounts.ToString() ?? "0";
                }

            }

            model.ToList().ForEach(a => a.IsFinaly = true);

            await UpdateRangeAsync(model, false);

            return Save();
        }

        /// <summary>
        /// زمانی که کاربر به سمت درگاه میرود 
        /// باید زمان درخواست این کالا به روز شود
        /// </summary>
        /// <param name="shopOrderId"></param>
        /// <returns></returns>
        public async Task UpdateCreateDate(int shopOrderId)
        {
            var results = await Entities.Where(x => x.ShopOrderId == shopOrderId).ToListAsync();
            results.ForEach(x => x.RequestedDate = DateTime.Now);
            await UpdateRangeAsync(results);
        }


        /// <summary>
        /// بررسی دوباره قیمت محصولات و ویرایش آن در سبد خرید
        /// </summary>
        /// <returns></returns>
        public async Task<SweetAlertExtenstion> ProductsPriceCheck(int invoiceId)
        {
            var model = await TableNoTracking.Include(a => a.Product).Where(a => a.ShopOrderId == invoiceId).ToListAsync();

            model.ForEach(a =>
            {
                a.OrderPrice = a.Product.Price.ToString();
                a.OrderPriceDiscount = a.Product.PriceWithDiscount.ToString();
            });

            await UpdateRangeAsync(model, false);

            return await SaveAsync();
        }

        /// <summary>
        /// ایجاد دوباره سبد خرید برای فاکتور
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<SweetAlertExtenstion> OverwriteShopProduct(int invoiceId, int orderId)
        {
            var model = await TableNoTracking.Include(a => a.Product).Where(a => a.ShopOrderId == invoiceId).ToListAsync();

            foreach (var item in model)
            {
                await AddAsync(new ShopProduct()
                {
                    Count = item.Count,
                    IsFactorSubmited = true,
                    ShopOrderId = orderId,
                    IsFinaly = false,
                    OrderName = item.OrderName,
                    OrderPrice = item.Product.Price.ToString(),
                    OrderPriceDiscount = item.Product.PriceWithDiscount.ToString(),
                    ProductId = item.ProductId,
                    UserId = item.UserId,
                    RequestedDate = DateTime.Now,

                }, false);
            }

            return await SaveAsync();
        }
    }

}
