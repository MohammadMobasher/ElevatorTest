using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO.Products
{
    public class ProductQueryFullDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int ProductGroupId { get; set; }

        public long Price { get; set; }

        public long PriceWithDiscount { get; set; }

        public int ParentId { get; set; }

        public string GroupTitle { get; set; }

    }

}
