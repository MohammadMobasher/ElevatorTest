using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO.ProductDiscounts
{
   public class PackageDiscountDTO
    {
        public int Id { get; set; }
        public long Discount { get; set; }

        public int? PackageId { get; set; }


        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ProductDiscountSSOT DiscountType { get; set; }

        public bool IsArchive { get; set; }

        public bool IsActive { get; set; }


        public DateTime? CreateDate { get; set; }
    }
}
