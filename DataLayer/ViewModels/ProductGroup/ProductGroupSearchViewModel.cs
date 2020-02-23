using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.ProductGroup
{
    public class ProductGroupSearchViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }

        public int? ParentId { get; set; }
    }
}
