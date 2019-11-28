using Core.Utilities;
using DataLayer.SSOT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using WebFramework.Authenticate;

namespace WebFramework.Base
{
    //[HasAccess]
    [Authorize]
    public class BaseAdminController : Controller
    {

        #region Fields

        /// <summary>
        /// شماره صفحه فعلی
        /// </summary>
        public int CurrentPage { get; set; } = 0;


        /// <summary>
        /// تعداد آیتم برای هر صفحه
        /// </summary>
        public int PageSize { get; set; } = 10;


        /// <summary>
        /// تعداد کل آیتم‌ها
        /// </summary>
        public int TotalNumber { get; set; }


        /// <summary>
        /// تعداد صفحات
        /// </summary>
        public int PageCount { get; set; }


        #endregion

        /// <summary>
        /// شماره کاربری شخصی که لاگین است
        /// </summary>
        public int UserId {
            get
            {
                if(_userId == null)
                    _userId = User.Identity.FindFirstValue(ClaimTypes.NameIdentifier).ToInt();
                return _userId.Value;
            }
        }

        private int? _userId { get; set; }

        public BaseAdminController()
        {

        }

        /// <summary>
        /// قبل از اجرا هر اکشنی این تابع اجرا میشود تا بتواند اطلاعات مورد نیاز که 
        /// از صفحه پاس داده شده است اینجا مقداردهی کند
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);


            this.CurrentPage = Request.Query["currentPage"].Count != 0 ? Convert.ToInt32(Request.Query["currentPage"][0]) : 1;

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
        }


    }
}
