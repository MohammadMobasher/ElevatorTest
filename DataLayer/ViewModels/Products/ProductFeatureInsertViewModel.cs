﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Products
{
    public class ProductFeatureInsertViewModel
    {
        public int ProductId { get; set; }

        public List<ProductFeatureItemViewModel> Items { get; set; }
    }

    public class PackageFeatureInsertViewModel
    {

        public List<PackageFeatureItemViewModel> Items { get; set; }
    }

    public class PackageIdAnswer
    {
        public int FeatureId { get; set; }

        public int Answer { get; set; }
    }
}
