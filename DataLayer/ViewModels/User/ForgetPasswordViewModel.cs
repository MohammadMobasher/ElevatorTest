using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModels.User
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "لطفا نام کاربری خود را وارد نمایید")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفا شماره تماس خود را وارد نمایید")]
        public string PhoneNumber { get; set; }
    }
}
