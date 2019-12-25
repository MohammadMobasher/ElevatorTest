using Core.Utilities;
using DataLayer.ViewModels.SideBar;
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


        public IViewComponentResult Invoke()
        {
            List<SidebarViewModel> items = new List<SidebarViewModel>();

            items.Add(new SidebarViewModel { Controller = "Home", Action = "Index", Title = "صفحه اصلی" });
            items.Add(new SidebarViewModel { Controller = "UserManage", Action = "Index", Title = "مدیریت کاربران", Icon = "zmdi-account-circle" });
            items.Add(new SidebarViewModel { Controller = "RoleManage", Action = "Index", Title = "مدیریت نقش ها", Icon = "zmdi-settings" });
            items.Add(new SidebarViewModel { Controller = "", Action = "", Title = "کالا", Icon = "zmdi-settings", Childs = new List<SidebarChildViewModel> {
                new SidebarChildViewModel {Controller = "Home", Action = "Index", Title = "صفحه اصلی" }
            } });
            
            //ViewBag.Controller = controller;
            //ViewBag.Action = action;
            //ViewBag.Icon = icon;
            //ViewBag.Title = title;

            return View(items);
        }
    }
}
