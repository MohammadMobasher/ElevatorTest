using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO.Products
{
    public class ProductPackageDetailFullDTO
    {
        public int PackageId { get; set; }

        public int ProductId { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<ProductPackage> ProductPackages { get; set; }
    }
}
