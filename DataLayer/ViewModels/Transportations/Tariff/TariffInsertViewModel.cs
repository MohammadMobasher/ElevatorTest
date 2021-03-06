﻿using DataLayer.BaseClasses;
using DataLayer.Entities.Transportation;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Transportations.Tariff
{
    public class TariffInsertViewModel : BaseMapping<TariffInsertViewModel, TransportationTariff>
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
        /// وزن کف محصول
        /// </summary>
        public double ProductSizeFrom { get; set; }

        /// <summary>
        /// وزن حد محصول
        /// </summary>
        public double ProductSizeTo { get; set; }


        /// <summary>
        /// تعرفه
        /// </summary>
        public long Tariff { get; set; }

    }
}
