﻿using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class ShopProduct:BaseEntity<int>
    {
        public int? ProductId { get; set; }

        public int? PackageId { get; set; }

        public int UserId { get; set; }

        public int Count { get; set; }

        public DateTime RequestedDate { get; set; }

        public bool IsFinaly { get; set; }

        public int? ShopOrderId { get; set; }


        public string OrderName { get; set; }

        public string OrderPrice { get; set; }

        public string OrderPriceDiscount { get; set; }


        /// <summary>
        /// آیا فاکتوری برای این سبد به ثبت رسیده است یا خیر
        /// </summary>
        public bool IsFactorSubmited { get; set; }

        [ForeignKey("PackageId")]
        public virtual ProductPackage ProductPackage { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product{ get; set; }

        [ForeignKey(nameof(ShopOrderId))]
        public virtual ShopOrder ShopOrder { get; set; }
    }
}
