using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.Product.Controllers
{

    [Area("Product")]
    [ControllerRole("مدیریت کالا‌ها")]
    public class ManageProductController : BaseAdminController
    {
        public ManageProductController(UsersAccessRepository usersAccessRepository) : base(usersAccessRepository)
        {

        }



        [ActionRole("صفحه کالاها")]
        [HasAccess]
        public IActionResult Index()
        {
            return View();
        }



    }
}