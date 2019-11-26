using Core.Utilities;
using DataLayer.Entities.Users;
using DataLayer.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Repos.User
{
    public class UsersAccessRepository : GenericRepository<UsersAccess>
    {
        private readonly RoleRepository _roleRepository;
        public UsersAccessRepository(DatabaseContext dbContext
            , RoleRepository roleRepository) : base(dbContext)
        {
            _roleRepository = roleRepository;
        }

        public bool HasAccess(int role, IDictionary<string, string> path)
        {
            var roleName = _roleRepository.GetRoleNameByRoleId(role);
            if (roleName == ImportantNames.AdminNormalTitle()) return true;

            var userAccess = TableNoTracking.Include(a => a.Roles).Where(a => a.Roles.Id == role).ToList();



            foreach (var item in userAccess)
            {
                if (item.Controller.ToUpper() == path["controller"].ToUpper() + ImportantNames.ControllerName())
                {
                    var actions = item.Actions == null ? null : JsonConvert.DeserializeObject<List<string>>(item.Actions);

                    if (actions != null && actions.Contains(path["action"])) return true;
                }
            }
            return false;
        }


        public bool HasAccess(int userId, string controller, string action) => true;
        //{
        //    var userAccess = TableNoTracking.Where(a => a.UserId == userId).ToList();

        //    foreach (var item in userAccess)
        //    {
        //        if (item.Controller.ToUpper() == controller.ToUpper() + ImportantNames.ControllerName())
        //        {
        //            var actions = item.Actions == null ? null : JsonConvert.DeserializeObject<List<string>>(item.Actions);

        //            if (actions != null && actions.Contains(action)) return true;
        //        }
        //    }
        //    return false;
        //}



        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public void AddAccessRole(List<UserAccessSubmitViewModel> vm, int roleId)
        {
            foreach (var item in vm)
            {
                if (!string.IsNullOrEmpty(item.Controller))
                {
                    Add(new UsersAccess()
                    {
                        RoleId = roleId,
                        Actions = item.Actions != null ? JsonConvert.SerializeObject(item.Actions) : null,
                        Controller = item.Controller
                    });
                }
            }
        }

        /// <summary>
        /// زمانی دسترسی های نقشی تغییر پیدا میکند ابتدا باید تمامی نقش های آن پاک شود
        /// </summary>
        /// <param name="roleId"></param>
        public void RemoveAccessRole(int roleId)
        {
            var lstAccessRole = TableNoTracking.Where(a => a.RoleId == roleId);
            if (lstAccessRole.Any()) DeleteRange(lstAccessRole);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public void UpdateAccessRole(List<UserAccessSubmitViewModel> vm, int roleId)
        {
            RemoveAccessRole(roleId);

            foreach (var item in vm)
            {
                if (!string.IsNullOrEmpty(item.Controller))
                {
                    Add(new UsersAccess()
                    {
                        RoleId = roleId,
                        Actions = item.Actions != null ? JsonConvert.SerializeObject(item.Actions) : null ,
                        Controller = item.Controller
                    });
                }
            }
        }
    }
}
