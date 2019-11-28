using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.SSOT
{
    public enum FilterUserSSOT
    {
        [Display(Name ="تمامی کاربران")]
        AllUser,

        [Display(Name = "کاربران فعال")]
        ActiveUser,

        [Display(Name ="کاربران غیر فعال")]
        DeActiveUser,

    }
}
