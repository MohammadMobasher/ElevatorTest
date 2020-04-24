using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.ProductDiscount
{
    public class PackageDiscountInsertViewModel
    {

        public decimal Discount { get; set; }

        public int? PackageId { get; set; }


        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ProductDiscountSSOT DiscountType { get; set; }

        public bool? IsArchive { get; set; } = false;

        public bool? IsActive { get; set; } = true;

        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
