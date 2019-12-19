using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.SSOT
{
    public enum ProductDiscountSSOT
    {
        [Display(Name = "درصدی")]
        Percent = 1,

        [Display(Name = "مبلغ")]
        Price = 2,
    }
}
