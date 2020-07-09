using DataLayer.BaseClasses;
using DataLayer.Entities.Transportation;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Transportations.Tariff
{
    public class TariffSearchViewModel 
    {
        /// <summary>
        /// از ناحیه --- 
        /// </summary>
        public TehranAreas? TehranAreasFrom { get; set; }

        /// <summary>
        ///  تا ناحیه ---
        /// </summary>
        public TehranAreas? TehranAreasTO { get; set; }

        /// <summary>
        /// ماشین
        /// </summary>
        public string CarName { get; set; }

        /// <summary>
        public double? ProductSizeTo { get; set; }
        /// وزن کف محصول
        /// </summary>
        public double? ProductSizeFrom { get; set; }

        /// <summary>
        /// وزن حد محصول
        /// </summary>


        /// <summary>
        /// تعرفه
        /// </summary>
        public long? Tariff { get; set; }

    }
}
