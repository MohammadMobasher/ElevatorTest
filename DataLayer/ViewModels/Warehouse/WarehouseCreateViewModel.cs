using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Warehouse
{
    public class WarehouseCreateViewModel
    {
        public int ProductId { get; set; }

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

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsActivity { get; set; }
    }
}
