﻿using DataLayer.BaseClasses;
using DataLayer.Entities.Transportation;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO.Transportations.Tariff
{
    public class TransportationTariffFullDto:BaseMapping<TransportationTariffFullDto,TransportationTariff,int>
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


        public virtual CarTransport CarTransport { get; set; }
    }
}
