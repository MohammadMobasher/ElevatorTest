using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Elevator.Helpers
{
    public static class ControllerExtention
    {

        public static int GetUserId(this ControllerBase controller)
        {
            return int.Parse(controller.User.Identity.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
