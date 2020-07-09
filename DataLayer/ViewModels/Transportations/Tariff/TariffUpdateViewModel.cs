using DataLayer.BaseClasses;
using DataLayer.Entities.Transportation;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Transportations.Tariff
{
    public class TariffUpdateViewModel : BaseMapping<TariffUpdateViewModel, TransportationTariff>
    {

        public int Id { get; set; }
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
        public int CarId { get; set; }

        /// <summary>
        /// وزن محصول
        /// </summary>
        public ProductSizeSSOT ProductSize { get; set; }

        /// <summary>
        /// تعرفه
        /// </summary>
        public long Tariff { get; set; }

    }
}
