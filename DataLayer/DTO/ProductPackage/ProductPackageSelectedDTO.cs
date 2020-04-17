using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO
{
    public class ProductPackageSelectedDTO
    {
        public int FeatureId { get; set; }
        public string FeatureValue { get; set; }

        public int ProductId { get; set; }

        public int ProductGroupId { get; set; }

    }
}
