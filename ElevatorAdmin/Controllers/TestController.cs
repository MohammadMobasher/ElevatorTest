using Microsoft.AspNetCore.Mvc;
using WebFramework.Base;

namespace ElevatorAdmin.Controllers
{
    public class TestController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}