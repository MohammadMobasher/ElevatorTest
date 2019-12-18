using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO.ProductFeatures
{
    public class ProductFeaturesFullDTO
    {
        public int FeatureId { get; set; }

        public int ProductId { get; set; }

        public string FeatureValue { get; set; }
    }
}
