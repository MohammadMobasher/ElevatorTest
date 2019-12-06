using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    /// <summary>
    /// ویژگی محصولات
    /// </summary>
    public class ProductFeature
    {
        public int Id { get; set; }

        public int FeatureId { get; set; }

        public int ProductId { get; set; }

        public string FeatureValue { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(FeatureId))]
        public virtual Feature Feature { get; set; }
    }
}
