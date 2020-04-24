using DataLayer.Entities.Common;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class ProductPackageDiscount : BaseEntity<int>
    {

        public long Discount { get; set; }

        public int? PackageId { get; set; }


        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ProductDiscountSSOT DiscountType { get; set; }

        public bool IsArchive { get; set; }

        public bool IsActive { get; set; }


        public DateTime? CreateDate { get; set; }


        [ForeignKey(nameof(PackageId))]
        public virtual ProductPackage ProductPackage{ get; set; }
    }
}
