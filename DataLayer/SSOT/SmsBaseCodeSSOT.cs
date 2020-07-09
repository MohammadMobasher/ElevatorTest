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
        Result = 19008,


        Ordered = 17335,
        Preparation = 17572,
        Loading = 17569,
        transmission = 17570,
        Delivery = 17571,
        TestSms = 17335,

    }
}
