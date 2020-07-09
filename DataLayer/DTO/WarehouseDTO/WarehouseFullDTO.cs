using DataLayer.Entities;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO.WarehouseDTO
{
    public class WarehouseFullDTO
    {
        public int Id { get; set; }

        /// <summary>
        /// عنوان
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// منطقه
        /// </summary>
        public TehranAreas Region { get; set; }

        /// <summary>
        /// آدرس
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// شماره تماس
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// فعالیت دارد
        /// </summary>
        public bool IsActivity { get; set; }

        public virtual Product Product { get; set; }
    }
}
