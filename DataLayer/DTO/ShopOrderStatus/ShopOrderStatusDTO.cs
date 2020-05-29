using DataLayer.Entities;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.DTO.ShopOrderStatus
{
    public class ShopOrderStatusDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// شماره فاکتور 
        /// </summary>
        public int ShopOrderId { get; set; }

        /// <summary>
        /// تاریخ و زمانی که در این وضعیت قرار گرفته است
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// وضعیت سفارش
        /// </summary>
        public ShopOrderStatusSSOT Status { get; set; }

        #region Relation

        [ForeignKey(nameof(ShopOrderId))]
        public virtual ShopOrder ShopOrder { get; set; }

        #endregion
    }
}
