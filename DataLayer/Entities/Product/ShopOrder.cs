using DataLayer.Entities.Common;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{

    /// <summary>
    /// سفارشات / فاکتور
    /// </summary>
    public class ShopOrder : BaseEntity
    {
        public DateTime? CreateDate { get; set; }

        public long Amount { get; set; }

        public bool IsSuccessed { get; set; }

        public DateTime? SuccessDate { get; set; }

        public int UserId { get; set; }

        public string DiscountCode { get; set; }

        public string OrderId { get; set; }

        public OrderStatusSSOT? Status{ get; set; }

        /// <summary>
        /// هزینه حمل و نقل
        /// </summary>
        public long? TransferProductPrice { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual Users.Users Users { get; set; }



    }
}
