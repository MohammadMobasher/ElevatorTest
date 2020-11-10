using Core.BankCommon.ViewModels;
using Core.Utilities;
using Dapper;
using DataLayer.DTO.RolesDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Service.Repos.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Base;

namespace ElevatorAdmin.Controllers
{
    public class TestController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        
    }
}