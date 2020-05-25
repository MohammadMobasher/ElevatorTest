using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="نام کاربری نمی تواند خالی باشد")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "رمز عبور خود را وارد کنید")]
        public string Password { get; set; }

        public bool IsRemember { get; set; }

    }
}
