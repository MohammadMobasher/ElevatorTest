﻿using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class ProductPackage : BaseEntity<int>
    {
        public string Title { get; set; }

        public long Discount { get; set; }

        public DateTime StartDiscount { get; set; }

        public DateTime EndDiscount { get; set; }

        public string ShortDiscription { get; set; }

        public string Text { get; set; }

        public string IndexPic { get; set; }

        public bool IsActive { get; set; }

        public int Visit { get; set; }

        public int Like { get; set; }

        public int DisLike { get; set; }

        public bool IsSpecialPackage { get; set; }

        public bool IsManager { get; set; } = false;


        public bool IsDeleted { get; set; } = false;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long PackagePrice { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long? PackageWithDiscounts { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual ICollection<ProductPackageDetails> ProductPackageDetails { get; set; }
    }
}
