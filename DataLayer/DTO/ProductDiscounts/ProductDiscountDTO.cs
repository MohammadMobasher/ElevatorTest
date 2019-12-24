using DataLayer.Entities;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO.ProductDiscounts
{
    public class ProductDiscountDTO
    {
        public int Id { get; set; }
        public long Discount { get; set; }

        public int? ProductId { get; set; }

        public int? ProductGroupId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ProductDiscountSSOT DiscountType { get; set; }

        public virtual Product Product { get; set; }

        public virtual ProductGroup ProductGroup { get; set; }
    }
}
