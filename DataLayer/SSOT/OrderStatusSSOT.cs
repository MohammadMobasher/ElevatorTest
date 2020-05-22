using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.SSOT
{
    public enum OrderStatusSSOT
    {
        [Display(Name = "در حال بررسی")]
        Check,

        [Display(Name = "درخواست رد شده")]
        Cancel,

        [Display(Name = "درحال بارگیری و ارسال")]
        AcceptAndReady,

        [Display(Name = "تحویل داده شده")]
        Receive,


    }
}
