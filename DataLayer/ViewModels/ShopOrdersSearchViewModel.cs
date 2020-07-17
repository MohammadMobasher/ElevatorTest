using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels
{
    public class ShopOrdersSearchViewModel
    {
        public long? Amount { get; set; }
        public long? OrderId { get; set; }
        public ShopOrderStatusSSOT? Status { get; set; }
        public bool? IsSuccessed { get; set; }

    }
}
