using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Products
{
    public class ProductFeatureItemViewModel
    {
        public int FeatureId { get; set; }

        public string FeatureValue { get; set; }
    }

    public class PackageFeatureItemViewModel
    {
        public int QuestionId { get; set; }
        public int FeatureId { get; set; }
        public string FeatureValue { get; set; }
    }
}
