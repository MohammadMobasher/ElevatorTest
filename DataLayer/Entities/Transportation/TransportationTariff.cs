using DataLayer.Entities.Common;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities.Transportation
{
    public class TransportationTariff : BaseEntity
    {
        /// <summary>
        /// از ناحیه --- 
        /// </summary>
        public TehranAreas TehranAreasFrom { get; set; }

        /// <summary>
        ///  تا ناحیه ---
        /// </summary>
        public TehranAreas TehranAreasTO { get; set; }

        /// <summary>
        /// ماشین
        /// </summary>
        public int? CarId { get; set; }

        /// <summary>
        /// وزن محصول
        /// </summary>
        public ProductSizeSSOT ProductSize { get; set; }

        /// <summary>
        /// تعرفه
        /// </summary>
        public long Tariff { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(CarId))]
        public virtual CarTransport CarTransport { get; set; }

    }
}
