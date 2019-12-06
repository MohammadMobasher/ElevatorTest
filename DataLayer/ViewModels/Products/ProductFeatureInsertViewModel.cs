using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Products
{
    public class ProductFeatureInsertViewModel
    {
        public int ProductId { get; set; }

        public List<ProductFeatureItemViewModel> Items { get; set; }
    }
}
