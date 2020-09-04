using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ElevatorAdminNewUI.Models;
using Core.CustomAttributes;
using WebFramework.Base;
using Service.Repos.User;

namespace ElevatorAdminNewUI.Controllers
{
    [ControllerRole("مدیریت داشبورد")]
    public class HomeController : BaseAdminController
    {
        public HomeController(UsersAccessRepository usersAccessRepository) : base(usersAccessRepository)
        {
        }

        [ActionRole("صفحه اصلی داشبورد")]
        [AllowAccess]
        //[HasAccess]
        public IActionResult Index()
        {
            ViewBag.User = User?.Identity?.Name;
            return View();
        }

        [ActionRole("تستی")]
        //[HasAccess]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //[HasAccess]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAccess]
        public IActionResult Error404()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAccess]
        public IActionResult Error403()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult TestFile()
        {
            return View();
        }
    }
}
