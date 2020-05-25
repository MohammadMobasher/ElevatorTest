using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModels.User
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "رمز عبور جاری")]
        [Required (ErrorMessage = "لطفا {0} را وارد نمایید")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [Display(Name = "تایید رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [Compare("NewPassword",ErrorMessage ="رمز عبور با تاییدش مغایرت دارد")]
        public string RePassword { get; set; }

    }
}
