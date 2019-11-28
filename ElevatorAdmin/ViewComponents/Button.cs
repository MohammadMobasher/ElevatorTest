using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Elevator.ViewComponents
{
    public class Button : ViewComponent
    {

        private readonly UsersAccessRepository _usersAccessRepository;

        public Button(UsersAccessRepository usersAccessRepository)
        {
            _usersAccessRepository = usersAccessRepository;
        }


        public IViewComponentResult Invoke(string controller , string action)
        {
            var userId = int.Parse(User.Identity.FindFirstValue(ClaimTypes.NameIdentifier));
            ViewBag.HasAccess = _usersAccessRepository.HasAccess(userId, controller, action);

            ViewBag.Controller = controller;
            ViewBag.Action = action;
            
            return View();
        }

    }
}
