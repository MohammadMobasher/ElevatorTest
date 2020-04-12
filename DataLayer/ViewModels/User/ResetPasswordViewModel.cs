using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModels.User
{
    public class ResetPasswordViewModel
    {
        public int AuthorityCode { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }

        public string Code { get; set; }
    }
}
