using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.SSOT
{
    public enum ProductSizeSSOT
    {
        [Display(Name = "خیلی سبک")]
        VeryLight,

        [Display(Name = "سبک")]
        Light,

        [Display(Name ="نیمه سبک")]
        SemiLight,

        [Display(Name ="وزن متوسط")]
        Normal,

        [Display(Name = "نیمه سنگین")]
        SemiHeavy,

        [Display(Name = "سنگین")]
        Heavy,

        [Display(Name = "خیلی سنگین")]
        VeryHeavy



    }
}
