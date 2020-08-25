using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.ShopProduct
{
    public class ShopProductAddWithShopOrderViewModel
    {

        public ShopProductAddWithShopOrderViewModel()
        {
            RequestedDate = DateTime.Now;
            IsFinaly = false;
        }

        public int ShopOrderId { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public int Count { get; set; }

        public DateTime RequestedDate { get; }

        public bool IsFinaly { get; }

        /// <summary>
        /// آیا فاکتوری برای این سبد به ثبت رسیده است یا خیر
        /// </summary>
        public bool IsFactorSubmited { get; set; }

    }
}
