using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extenstions
{
    public class SweetAlertExtenstion
    {
        public string Message { get; set; }
        public bool Succeed { get; set; }

        public static SweetAlertExtenstion Ok(string message = null)
        {
            return new SweetAlertExtenstion()
            {
                Message = message ?? "اطلاعات با موفقیت ثبت شد",
                Succeed = true
            };
        }
        public static SweetAlertExtenstion Error(string message = null)
        {
            return new SweetAlertExtenstion()
            {
                Message = message ?? "خطایی در سایت رخ داده است",
                Succeed = false
            };
        }



    }
}
