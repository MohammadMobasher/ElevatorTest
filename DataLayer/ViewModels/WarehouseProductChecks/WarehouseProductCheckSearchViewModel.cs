using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.WarehouseProductChecks
{
    public class WarehouseProductCheckSearchViewModel
    {

        /// <summary>
        /// عنوان محصول
        /// </summary>
        public string ProductTitle { get; set; }

        /// <summary>
        /// ورودی یا خروجی
        /// </summary>
        public WarehouseTypeSSOT? Type { get; set; }

        /// <summary>
        /// تعداد
        /// </summary>
        public int? Count { get; set; }

    }
}
