﻿using Core.Utilities;
using DataLayer.Entities.Users;
using DataLayer.SSOT;
using DataLayer.ViewModels.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos.User
{
    public class UsersRoleRepository : GenericRepository<UserRoles>
    {
        public UsersRoleRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// تنظیم نقش کاربر
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public SweetAlertExtenstion SetRole(SetUserRoleViewModel vm)
        {
            var userRole = TableNoTracking.FirstOrDefault(a => a.UserId == vm.UserId);

            return ResetRole();

            #region LocalMethod

            SweetAlertExtenstion ResetRole()
            {
                if (userRole == null) Add(vm, false); 
                else
                {
                    Delete(userRole);
                    Add(vm, false);
                }

                return Save();
            }

            #endregion
        }




    }
}
