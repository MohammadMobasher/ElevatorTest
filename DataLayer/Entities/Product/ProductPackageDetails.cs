using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class ProductPackageDetails :BaseEntity<int>
    {
        public int PackageId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(PackageId))]
        public virtual ProductPackage ProductPackage { get; set; }

    }
}
