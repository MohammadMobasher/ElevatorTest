using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entities
{
    public class FactorAndPackage : BaseEntity<int>
    {

        /// <summary>
        /// زمان ثبت فاکتور
        /// </summary>
        [Required]
        public DateTime Date { get; set; }


        /// <summary>
        /// جمع کل فاکتور
        /// </summary>
        public int TotalAmount { get; set; }


        /// <summary>
        /// شماره کاربری که این قاکتور به نام آن صادر شده است
        /// </summary>
        public int UserId { get; set; }


        /// <summary>
        /// آیا این رکورد فاکتور است یا پکیج
        /// برای تعریف پکیج‌های مختلف از همین جدول استفاده خواهد شد
        /// </summary>
        public bool IsFactor { get; set; }

    }
}
