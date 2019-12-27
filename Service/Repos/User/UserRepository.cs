using Core.Utilities;
using DataLayer.Entities.Users;
using DataLayer.SSOT;
using DataLayer.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        /// گرفتن کاربران شرایط خاص
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IQueryable<Users> GetUsers(FilterUserSSOT filter)
        {
            var model = TableNoTracking
                .WhereIf(filter == FilterUserSSOT.AllUser, a => true)
                .WhereIf(filter == FilterUserSSOT.ActiveUser, a => a.IsActive)
                .WhereIf(filter == FilterUserSSOT.DeActiveUser, a => !a.IsActive);

            return model;
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

        /// <summary>
        /// گرفتن اطلاعات شماره تماس تمامی کاربران
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> AllUserPhoneNumber()
        {
            var phoneNumbers = await TableNoTracking.Select(a => a.PhoneNumber).ToListAsync();

            return phoneNumbers;
        }

        /// <summary>
        /// گرفتن اطلاعات شماره تماس تمامی کاربران
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> AllUserPhoneNumber(FilterUserSSOT filter)
        {
            var phoneNumbers = GetUsers(filter);

            return await phoneNumbers.Select(a => a.PhoneNumber).ToListAsync();
        }



        /// <summary>
        /// تعداد تمامی کاربران
        /// </summary>
        /// <returns></returns>
        public int CountUsers() => TableNoTracking.Count();

        public async Task<SweetAlertExtenstion> ChangeUserActivity(int userId)
        {
            try
            {
                var userInfo = await GetByIdAsync(userId);

                if (userInfo == null) return SweetAlertExtenstion.Error("کاربری با این شناسه یافت نشد");

                userInfo.IsActive = !userInfo.IsActive;
                await UpdateAsync(userInfo);

                return SweetAlertExtenstion.Ok();
            }
            catch (Exception)
            {
                return SweetAlertExtenstion.Error("خطای نامشخصی رخ داده است لطفا پس از چند لحظه دوباره امتحان کنید و در صورت برطرف نشدن مشکل با پشتیبانی تماس بگیرید");
            }
        }


        public async Task<ClaimsPrincipal> SetUserClaims(string username)
        {
            var userinfo = await GetByConditionAsync(a => a.UserName == username);

            var claimsidentity = new ClaimsIdentity(new[]
                {
                        new Claim("FirstName", userinfo.FirstName ?? "1"),
                        new Claim("LastName",  userinfo.LastName ?? "2"),
                        new Claim("FullName",  userinfo.FirstName + " 3"+ userinfo.LastName),
                        new Claim("UserProfile" , userinfo.ProfilePic ?? "/Uploads/UserImage/NoPhoto.jpg")
                        //...
                }, ".Elevator.Cookies");

            return new ClaimsPrincipal(claimsidentity);
        }


    }
}
