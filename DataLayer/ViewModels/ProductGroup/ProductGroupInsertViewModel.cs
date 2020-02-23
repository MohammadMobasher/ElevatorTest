using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.ProductGroup
{
    public class ProductGroupInsertViewModel
    {
        public string Title { get; set; }

        public int? ParentId { get; set; }
    }
}
