using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Core;
using Core.Utilities;
using System.Security.Claims;
using Microsoft.AspNetCore.Routing;

namespace Elevator.Controllers
{
    public class BaseController : Controller
    {

        #region Fields

        /// <summary>
        /// شماره صفحه فعلی
        /// </summary>
        public int CurrentPage { get; set; } = 0;


        /// <summary>
        /// تعداد آیتم برای هر صفحه
        /// </summary>
        public int PageSize { get; set; } = 12;


        /// <summary>
        /// تعداد کل آیتم‌ها
        /// </summary>
        public int TotalNumber { get; set; }


        /// <summary>
        /// تعداد صفحات
        /// </summary>
        public int PageCount { get; set; }

        private int? _userId;
        public int UserId
        {
            get
            {
                if (_userId == null)
                    _userId = User.Identity.FindFirstValue(ClaimTypes.NameIdentifier).ToInt();
                return _userId.Value;
            }
        }


        #endregion


        public BaseController()
        {

        }

        /// <summary>
        /// قبل از اجرا هر اکشنی این تابع اجرا میشود تا بتواند اطلاعات مورد نیاز که 
        /// از صفحه پاس داده شده است اینجا مقداردهی کند
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //var d = User.Identity;
            //if (string.IsNullOrEmpty(User.Identity.FindFirstValue("FirstName")))
            //{
            //    context.Result =
            //        new RedirectToRouteResult(new RouteValueDictionary(new
            //        {
            //            controller = "Home",
            //            action = "Login11111"
            //        }));
            //}

            //if (context.HttpContext.Session == null ||
            //        !context.HttpContext.Session.TryGetValue("ID", out byte[] val))
            //{
            //    context.Result =
            //        new RedirectToRouteResult(new RouteValueDictionary(new
            //        {
            //            controller = "Home",
            //            action = "Login11111"
            //        }));
            //}
            base.OnActionExecuting(context);


            this.CurrentPage = Request.Query["currentPage"].Count != 0 ? Convert.ToInt32(Request.Query["currentPage"][0]) : 1;
            this.PageSize = Request.Query["pageSize"].Count != 0 ? Convert.ToInt32(Request.Query["pageSize"][0]) : 12;

        }



        /// <summary>
        /// بعد از هر اکشنی این تابع صدا زده می‌شود
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);


            ViewBag.CurrentPage = this.CurrentPage;
            ViewBag.pageSize = this.PageSize;


            ViewBag.pageCount = (int)Math.Floor((decimal)((this.TotalNumber + this.PageSize - 1) / this.PageSize));
            ViewBag.totalNumber = this.TotalNumber;


            //ViewBag.Url = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }


    }
}
