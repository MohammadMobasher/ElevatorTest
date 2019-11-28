using Core.Utilities;
using DataLayer.Entities.Users;
using DataLayer.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos.User
{
    public class UserRepository : GenericRepository<Users>
    {
        private readonly UserManager<Users> _userManager;
        public UserRepository(DatabaseContext db) : base(db)
        {

        }
        public UserRepository(DatabaseContext db, UserManager<Users> userManager) : base(db)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// تغییر رمز عبور توسط مدیریت
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public async Task<SweetAlertExtenstion> AdminChangePassword(AdminSetPasswordViewModel vm)
        {
            var model = await GetByIdAsync(vm.UserId);

            if (model == null) return SweetAlertExtenstion.Error("کاربری با این شناسه یافت نشد");

            var newHashPassword = _userManager.PasswordHasher.HashPassword(model, vm.Password);
            model.PasswordHash = newHashPassword;
            var change = await _userManager.UpdateAsync(model);

            return change.Succeeded ? SweetAlertExtenstion.Ok() : SweetAlertExtenstion.Error();
        }

        /// <summary>
        /// گرفتن اطلاعات شماره تماس بر اساس شناسه کاربری
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> PhoneNumberByUserId(int userId)
        {
           var userInfo = await GetByIdAsync(userId);

            return userInfo.PhoneNumber;
        }

        //public async Task<SweetAlertExtenstion> ChangeUserActivity(int userId)
        //{
        //    var userInfo = await GetByIdAsync(userId);

        //    userInfo.
        //}

    }
}
