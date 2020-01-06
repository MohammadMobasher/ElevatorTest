﻿using DataLayer.Entities;
using DataLayer.Entities.Warehouse;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO.WarehouseProductCheckDTO
{
    public class WarehouseProductCheckFullDTO
    {
        public int Id { get; set; }
        public int WarehouseId { get; set; }

        public int ProductId { get; set; }

        /// <summary>
        /// تعداد ورود / خروج محصول
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// ورود / خروج
        /// </summary>
        public WarehouseTypeSSOT TypeSSOt { get; set; }

        /// <summary>
        /// تاریخ ورود / خروج
        /// </summary>
        public DateTime Date { get; set; }


        /// <summary>
        /// فایل فاکتور
        /// </summary>
        public string Factor { get; set; }

        /// <summary>
        /// نام تحویل گیرنده / تحویل دهنده
        /// </summary>
        public string FirstName { get; set; }


        /// <summary>
        /// نام خانوداگی تحویل گیرنده /تحویل دهنده
        /// </summary>
        public string LastName { get; set; }

        #region NavigationProperty

        public virtual Warehouse Warehouse { get; set; }

        public virtual Product Product { get; set; }

        #endregion
    }
}
