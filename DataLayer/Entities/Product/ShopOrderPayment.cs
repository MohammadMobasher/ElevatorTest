using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class ShopOrderPayment:BaseEntity<int>
    {
        /// <summary>
        /// شماره فاکتور
        /// </summary>
        public int ShopOrderId { get; set; }

        public long PaymentAmount { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string OrderId { get; set; }


        public bool IsSuccess { get; set; }

        /// <summary>
        ///  آیا پرداخت آنلاین می باشد یا پرداخت درب منزل؟
        /// </summary>
        public bool IsOnlinePay { get; set; }

        public DateTime? SuccessDate { get; set; }

        [ForeignKey(nameof(ShopOrderId))]
        public virtual ShopOrder ShopOrder { get; set; }
    }
}
