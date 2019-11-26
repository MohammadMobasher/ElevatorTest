using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.Entities.Users;
using DataLayer.ViewModels.Role;
using DataLayer.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos.User
{
    public class RoleRepository : GenericRepository<Roles>
    {
        public RoleRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        //public async Task<SweetAlertExtenstion> CheckAndSync(params Assembly[] assemblies)
        //{
        //    var controllerInfo = GetDisplayAndNameController(assemblies);

        //    var roles = TableNoTracking.Select(a => a.Name).ToList();

        //    var roleList = new List<Roles>();

        //    foreach (var item in controllerInfo)
        //    {
        //        roleList.Add(new Roles()
        //        {
        //            Name = item.Name,
        //            ConcurrencyStamp = Guid.NewGuid().ToString(),
        //            NormalizedName = item.Name.ToUpper().Trim(),
        //            RoleTitle = item.GetCustomAttribute<ControllerRoleAttribute>()?.GetName()
        //        });
        //    }

        //    roleList = roleList.Where(a => !roles.Contains(a.Name)).ToList();

        //    await AddRangeAsync(roleList);

        //    return SweetAlertExtenstion.Ok("تمامی اطلاعات ثبت شد");
        //}


        ///// <summary>
        ///// گرفتن تایپ کنترلر هایی مورد نظر
        ///// </summary>
        ///// <param name="assemblies"></param>
        ///// <returns></returns>
        //public List<Type> GetDisplayAndNameController(params Assembly[] assemblies)
        //      => typeof(ControllerRoleAttribute).GetTypesHasAttribute(assemblies).ToList();


        public string GetRoleNameByRoleId(int roleId)
              => TableNoTracking.FirstOrDefault(a => a.Id == roleId).NormalizedName;


        /// <summary>
        /// افزودن یک نقش 
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="roleTitle"></param>
        /// <returns></returns>
        public async Task<int> AddRole(RoleInsertViewModel vm)
        {
            var role = new Roles()
            {
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Name = vm.Name,
                NormalizedName = vm.Name.Trim().ToUpper(),
                RoleTitle = vm.RoleTitle
            };

            await AddAsync(role);

            return role.Id;
        }

        /// <summary>
        /// ویرایش یک نقش 
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="roleTitle"></param>
        /// <returns></returns>
        public void UpdateRole(RoleUpdateViewModel vm)
        {
            var model = Map(vm);
            model.NormalizedName = vm.Name.Trim().ToUpper();
            model.ConcurrencyStamp = Guid.NewGuid().ToString();

            DbContext.SaveChanges();
        }
    }
}
