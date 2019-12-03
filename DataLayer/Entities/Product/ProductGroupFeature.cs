using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class ProductGroupFeature : BaseEntity<int>
    {
        
        public int ProductGroupId { get; set; }

        public int FeatureId { get; set; }



        #region Join

        [ForeignKey(nameof(ProductGroupId))]
        public virtual ProductGroup ProductGroup { get; set; }

        [ForeignKey(nameof(FeatureId))]
        public virtual Feature Feature { get; set; }

        #endregion


    }
}
