using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.ShopOrderStatus
{
    public class ShopOrderStatusInsertViewModel
    {

        /// <summary>
        /// شماره فاکتور 
        /// </summary>
        public int ShopOrderId { get; set; }

        /// <summary>
        /// تاریخ و زمانی که در این وضعیت قرار گرفته است
        /// </summary>
        public DateTime Date { get; set; } = DateTime.Now;

        /// <summary>
        /// وضعیت سفارش
        /// </summary>
        public ShopOrderStatusSSOT Status { get; set; }

    }
}
