using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.WarehouseProductChecks
{
    public class WarehouseProductCheckInsertViewModel
    {
        public WarehouseProductCheckInsertViewModel()
        {
            Date = DateTime.Now;
        }
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

    }
}
