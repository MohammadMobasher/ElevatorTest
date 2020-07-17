using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.SSOT
{
    public enum ShopOrderStatusSSOT
    {

        Nothing = -1,
        /// <summary>
        /// ثبت سفارش
        /// </summary>
        [Display(Name ="ثبت سفارش")]
        Ordered = 0,
        /// <summary>
        /// آماده سازی
        /// </summary>
        [Display(Name ="آماده سازی")]
        Preparation = 1,
        /// <summary>
        /// بارگیری
        /// </summary>
        [Display(Name = "بارگیری")]
        Loading = 2,
        /// <summary>
        /// ارسال
        /// </summary>
        [Display(Name = "ارسال")]
        transmission = 3,
        /// <summary>
        /// تحویل
        /// </summary>
        [Display(Name = "تحویل")]
        Delivery = 4




    }
}
