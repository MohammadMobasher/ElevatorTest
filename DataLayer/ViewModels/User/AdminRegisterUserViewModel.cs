using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModels.User
{
    public class AdminRegisterUserViewModel
    {
        public AdminRegisterUserViewModel()
        {
            IsModerator = true;
        }

        [Required(ErrorMessage = "نام کاربری نمی تواند خالی باشد")]
        [StringLength(150,ErrorMessage = "تعداد کاراکتر ها بیشتر از حد مجاز می باشد")]
        [RegularExpression(@"[A-Za-z0-9]+", ErrorMessage = "برای نام کاربری حتما باید از کاراکتر های لاتین استفاده کنید")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفا ایمیل را وارد نمایید")]
        [StringLength(500,ErrorMessage = "تعداد کاراکتر ها بیشتر از حد مجاز می باشد")]
        [EmailAddress(ErrorMessage = "لطفا ایمیل خود را با فرمت صحیح وارد کنید")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا نام را وارد نمایید")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد نمایید")]
        public string LastName { get; set; }

        public bool IsModerator { get; set; }
    }
}
