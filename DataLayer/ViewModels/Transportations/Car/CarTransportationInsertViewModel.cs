using DataLayer.BaseClasses;
using DataLayer.Entities.Transportation;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Transportations.Car
{
    public class CarTransportationInsertViewModel: BaseMapping<CarTransportationInsertViewModel, CarTransport>
    {
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

        /// <summary>
        /// فعال / غیر فعال
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// آیا این ماشین حذف شده است یا خیر
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
