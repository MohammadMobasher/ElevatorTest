﻿using DataLayer.Entities;
using DataLayer.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos.User
{
    public class UserAddressRepository : GenericRepository<UserAddress>
    {
        public UserAddressRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// چک کردن اینکه برای این شخص قبلا آدرسی ذخیره شده یا خیر
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Check(int userId)
            => TableNoTracking.Any(a => a.UserId == userId);
        
        public bool Submit(UserAddress vm)
        {

            var result = this.Entities.SingleOrDefault(x => x.ShopOrderId == vm.ShopOrderId);
            if(result != null)
            {
                this.DbContext.UserAddress.Remove(result);
            }
            //if (!Check(vm.UserId))
            //{
            vm.Id = 0;

                Add(vm,false);
            //}
            //else
            //{
            //    Update(vm, false);
            //}

            return Save();
        }


        public async Task<bool> UpdateShopOrderId(int shopOrderId, int userId)
        {

            var entity = await DbContext.UserAddress.LastOrDefaultAsync(x => x.UserId == userId && x.ShopOrderId == null);

            if (entity == null)
            {
                await DbContext.UserAddress.AddAsync(new UserAddress
                {
                    ShopOrderId = shopOrderId,
                    UserId = userId,

                });
                await DbContext.SaveChangesAsync();
                return true;
            }
            entity.ShopOrderId = shopOrderId;
            await DbContext.SaveChangesAsync();

            return true;
        }


        
    }
}
