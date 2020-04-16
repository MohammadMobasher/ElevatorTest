using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class ProductPackageGroups : BaseEntity<int>
    {
        public int PackageId { get; set; }

        public int GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        public virtual ProductGroup ProductGroup { get; set; }

        [ForeignKey(nameof(PackageId))]
        public virtual ProductPackage ProductPackage { get; set; }

    }
}
