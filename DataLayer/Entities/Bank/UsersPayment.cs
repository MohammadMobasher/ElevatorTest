using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities.Bank
{
    public class UsersPayment : BaseEntity
    {
        public int UserId { get; set; }

        public int OrderId { get; set; }

        public DateTime DateTime { get; set; }

        public long Amount { get; set; }

        public int MyProperty { get; set; }

        public string Token { get; set; }


        public virtual Users.Users Users { get; set; }
    }
}
