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

        public bool IsSuccess { get; set; }

        [ForeignKey(nameof(ShopOrderId))]
        public virtual ShopOrder ShopOrder { get; set; }
    }
}
