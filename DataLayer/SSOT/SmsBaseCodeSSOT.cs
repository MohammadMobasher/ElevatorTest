using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.SSOT
{
    public enum SmsBaseCodeSSOT
    {
        /// <summary>
        /// ثبت نام
        /// </summary>
        Register = 14508,

        Order = 14509,
        /// <summary>
        /// فراموشی رمز عبور
        /// </summary>
        ForgetPassword = 14608,
        /// <summary>
        /// ثبت ثبت فاکتور
        /// </summary>
        SetOrder = 14509,
        /// <summary>
        ///  نتیجه درگاه
        /// </summary>
        Result = 15920,
        /// <summary>
        /// سفارش انجام شد
        /// </summary>
        Ordered = 17335,
        Preparation = 17572,
        /// <summary>
        /// بارگری
        /// </summary>
        Loading = 21240,
        /// <summary>
        /// ارسال
        /// </summary>
        transmission = 17570,
        /// <summary>
        /// تحویل
        /// </summary>
        Delivery = 17571,
        /// <summary>
        /// تستی
        /// </summary>
        TestSms = 17335,

    }
}
