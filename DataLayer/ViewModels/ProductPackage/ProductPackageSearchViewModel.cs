using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Products
{
    public class ProductPackageSearchViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }

        public long? Discount { get; set; }

        public DateTime StartDiscount { get; set; }

        public DateTime EndDiscount { get; set; }

        public string ShortDiscription { get; set; }

        public string Text { get; set; }

        public string IndexPic { get; set; }

        public int? Likes { get; set; }
    }
}
