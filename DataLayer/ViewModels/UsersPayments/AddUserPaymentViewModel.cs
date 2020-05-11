using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.UsersPayments
{
    public class AddUserPaymentViewModel
    {
        public int UserId { get; set; }

        public string OrderId { get; set; }

        public DateTime DateTime { get; set; }

        public long Amount { get; set; }


        public string Token { get; set; }
    }
}
