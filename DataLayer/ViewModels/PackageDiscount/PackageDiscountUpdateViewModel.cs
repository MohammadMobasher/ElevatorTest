using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.ProductDiscount
{
    public class PackageDiscountUpdateViewModel
    {
        public int Id { get; set; }
        public decimal Discount { get; set; }

        public int? PackgeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ProductDiscountSSOT DiscountType { get; set; }
    }
}
