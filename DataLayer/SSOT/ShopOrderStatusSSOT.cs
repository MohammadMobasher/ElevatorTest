using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.SSOT
{
    public enum ShopOrderStatusSSOT
    {
        Nothing = -1,
        /// <summary>
        /// ثبت سفارش
        /// </summary>
        Ordered = 0,
        /// <summary>
        /// آماده سازی
        /// </summary>
        Preparation = 1,
        /// <summary>
        /// بارگیری
        /// </summary>
        Loading = 2,
        /// <summary>
        /// ارسال
        /// </summary>
        transmission = 3,
        /// <summary>
        /// تحویل
        /// </summary>
        Delivery = 4




    }
}
