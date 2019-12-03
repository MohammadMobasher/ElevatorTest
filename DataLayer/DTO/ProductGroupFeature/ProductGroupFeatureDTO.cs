using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO.ProductGroupFeature
{
    public class ProductGroupFeatureDTO
    {
        public int Id { get; set; }
        public int ProductGroupId { get; set; }
        public int FeatureId { get; set; }
        public string FeatureTitle { get; set; }

    }
}
