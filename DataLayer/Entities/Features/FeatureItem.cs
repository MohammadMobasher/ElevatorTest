using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    /// <summary>
    /// گرفتن اطلاعات
    /// </summary>
    public class FeatureItem : BaseEntity<int>
    {
        public string Value { get; set; }
        public int FeatureId { get; set; }

        [ForeignKey(nameof(FeatureId))]
        public virtual Feature Feature { get; set; }
    }
}
