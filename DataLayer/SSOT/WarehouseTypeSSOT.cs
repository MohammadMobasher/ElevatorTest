using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.SSOT
{
    public enum WarehouseTypeSSOT
    {
        [Display(Name = "ورودی")]
        In = 1,

        [Display(Name = "خروجی")]
        Out = 2
    }
}
