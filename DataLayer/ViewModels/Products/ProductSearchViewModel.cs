using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Products
{
    public class ProductSearchViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }

        public long? Price { get; set; }

        public int? ProductGroupId { get; set; }

        public int? Likes { get; set; }
    }
}
