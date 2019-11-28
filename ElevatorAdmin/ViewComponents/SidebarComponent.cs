using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ElevatorAdmin.ViewComponents
{
    public class SidebarComponent : ViewComponent
    {
        private readonly UsersAccessRepository _usersAccessRepository;
        private readonly UsersRoleRepository _usersRoleRepository;
        public SidebarComponent(UsersAccessRepository usersAccessRepository
            , UsersRoleRepository usersRoleRepository)
        {
            _usersAccessRepository = usersAccessRepository;
            _usersRoleRepository = usersRoleRepository;
        }


        public IViewComponentResult Invoke(string controller, string action, string title, string icon = "zmdi-view-dashboard")
        {
            var userId = User.Identity.FindFirstValue(ClaimTypes.NameIdentifier) != null 
                ? int.Parse(User.Identity.FindFirstValue(ClaimTypes.NameIdentifier)) : 0;

            bool hassAccess = false;

            if (userId != 0)
            {
                var role = _usersRoleRepository.TableNoTracking.FirstOrDefault(a => a.UserId == userId).RoleId;
                hassAccess = _usersAccessRepository.HasAccess(role, controller, action);
            }

            // تصمیم گیری نهایی که برای ما سرعت مهم است یا امنیت
            //var roleId = int.Parse(User.Identity.FindFirstValue(ClaimTypes.NameIdentifier));
            //ViewBag.HasAccess = _usersAccessRepository.HasAccess(roleId, controller, action);

            ViewBag.HasAccess = hassAccess;
            ViewBag.Controller = controller;
            ViewBag.Action = action;
            ViewBag.Icon = icon;
            ViewBag.Title = title;

            return View();
        }
    }
}
