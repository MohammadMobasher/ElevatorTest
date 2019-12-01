using Microsoft.AspNetCore.Mvc;
using Service.Repos.User;
using WebFramework.Base;

namespace ElevatorAdmin.Controllers
{
    public class TestController : BaseAdminController
    {
        public TestController(UsersAccessRepository usersAccessRepository) : base(usersAccessRepository)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}