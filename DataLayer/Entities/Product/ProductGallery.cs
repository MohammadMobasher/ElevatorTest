using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{

    /// <summary>
    /// عکس های محصول
    /// </summary>
    public class ProductGallery : BaseEntity<int>
    {
        public string Pic { get; set; }

        public int ProductId { get; set; }

        #region Join

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        #endregion
    }
}
