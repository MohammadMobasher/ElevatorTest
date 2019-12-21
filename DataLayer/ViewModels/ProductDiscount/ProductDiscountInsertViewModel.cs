using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.ProductDiscount
{
    public class ProductDiscountInsertViewModel
    {

        public decimal Discount { get; set; }

        public int? ProductId { get; set; }

        public int? ProductGroupId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ProductDiscountSSOT DiscountType { get; set; }
    }
}
