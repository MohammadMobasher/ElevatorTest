using DataLayer.BaseClasses;
using DataLayer.Entities.Transportation;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Transportations.Car
{
    public class CarTransportaionUpdateViewModel : BaseMapping<CarTransportaionUpdateViewModel, CarTransport>
    {
        public int Id { get; set; }
        /// <summary>
        /// نام وسیله
        /// </summary>
        public string CarName { get; set; }

        /// <summary>
        /// مدل وسیله
        /// </summary>
        public string CarModel { get; set; }

        /// <summary>
        /// شماره پلاک
        /// </summary>
        public string Plaque { get; set; }

        /// <summary>
        /// شماره موتور
        /// </summary>
        public string MotorSerial { get; set; }

        /// <summary>
        /// بیشترین وزن قابل تحمل
        /// </summary>
        public ProductSizeSSOT TransportSize { get; set; }

        /// <summary>
        /// عکس از وسیله نقلیه
        /// </summary>
        public string Pic { get; set; }
    }
}
