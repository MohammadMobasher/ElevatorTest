using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities.Bank
{
    public class UsersPayment : BaseEntity
    {
        public int UserId { get; set; }

        public string OrderId { get; set; }

        public DateTime DateTime { get; set; }

        public long Amount { get; set; }

        public string Token { get; set; }

        public bool IsSuccessed { get; set; }

        public int? ShopOrderId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual Users.Users Users { get; set; }


        [ForeignKey(nameof(ShopOrderId))]
        public virtual ShopOrder ShopOrders { get; set; }
    }
}
