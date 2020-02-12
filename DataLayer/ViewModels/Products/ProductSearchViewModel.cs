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

    public class ProductSearchListViewModel
    {
        public string Titile { get; set; }

        public int? Group { get; set; }

        public int? SubGroup { get; set; }

        public string Price { get; set; }

        public int? Unit { get; set; }

    }
}
