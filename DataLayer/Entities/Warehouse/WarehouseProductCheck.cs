using DataLayer.Entities.Common;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities.Warehouse
{
    /// <summary>
    /// مدیریت ورودی خروجی انبار
    /// </summary>
    public class WarehouseProductCheck:BaseEntity<int>
    {
        public int WarehouseId { get; set; }

        public int ProductId { get; set; }

        /// <summary>
        /// تعداد ورود / خروج محصول
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// ورود / خروج
        /// </summary>
        public WarehouseTypeSSOT TypeSSOt { get; set; }

        /// <summary>
        /// تاریخ ورود / خروج
        /// </summary>
        public DateTime Date { get; set; }


        /// <summary>
        /// فایل فاکتور
        /// </summary>
        public string Factor { get; set; }

        /// <summary>
        /// نام تحویل گیرنده / تحویل دهنده
        /// </summary>
        public string FirstName { get; set; }


        /// <summary>
        /// نام خانوداگی تحویل گیرنده /تحویل دهنده
        /// </summary>
        public string LastName { get; set; }



        [ForeignKey(nameof(WarehouseId))]
        public virtual Warehouse Warehouse { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
    }
}
