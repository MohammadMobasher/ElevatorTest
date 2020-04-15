using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO.Products
{
    public class ProductPackageFullDTO
    {
        public int Id { get; set; }
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

        public long PackagePrice { get; set; }

        public DateTime CreateDate { get; set; }

        public ICollection<ProductPackageDetailFullDTO> ProductPackageDetails { get; set; }
    }
}
