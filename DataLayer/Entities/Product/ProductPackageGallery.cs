using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class ProductPackageGallery : BaseEntity<int>
    {
        public int PackageId { get; set; }

        public string File { get; set; }

        [ForeignKey(nameof(PackageId))]
        public virtual ProductPackage ProductPackage { get; set; }
    }
}
