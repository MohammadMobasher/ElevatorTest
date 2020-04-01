using DataLayer.Entities.Common;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class ProductDiscount : BaseEntity<int>
    {
   
        public long Discount { get; set; }

        public int? ProductId { get; set; }

        public int? ProductGroupId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ProductDiscountSSOT DiscountType { get; set; }

        public bool? IsArchive { get; set; }

        public bool? IsActive { get; set; }


        public DateTime? CreateDate { get; set; }


        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(ProductGroupId))]
        public virtual ProductGroup ProductGroup { get; set; }

    }
}
