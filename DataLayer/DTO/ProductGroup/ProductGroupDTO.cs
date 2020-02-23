using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO
{
    public class ProductGroupDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }


        public int? ParentId { get; set; } = -1;

        public virtual ICollection<Product> Products { get; set; }


        public virtual ProductGroup Parent { get; set; }


    }
}
