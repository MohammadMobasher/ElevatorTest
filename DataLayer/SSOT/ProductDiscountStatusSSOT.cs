using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.SSOT
{
    public enum ProductDiscountStatusSSOT
    {
        [Display(Name = "فعال")]
        Active = 1,

        [Display(Name = "غیر فعال")]
        DeActive = 2,

        [Display(Name ="حذف شده")]
        Removed = 3,

        [Display(Name = "منقضی شده")]
        Expierd = 4
    }
}
