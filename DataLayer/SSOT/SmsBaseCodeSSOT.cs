using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.SSOT
{
    public enum SmsBaseCodeSSOT
    {
        Register = 14508,

        Order = 14509,

        ForgetPassword = 14608,
        /// <summary>
        /// ثبت سفارش
        /// </summary>
        SetOrder = 14509,
        /// <summary>
        ///  نتیجه درگاه
        /// </summary>
        Result = 15920,


        Ordered = 16031,
        Preparation = 16712,
        Loading = 16032,
        transmission = 16033,
        Delivery = 16034

    }
}
