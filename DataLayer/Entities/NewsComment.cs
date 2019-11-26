using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class NewsComment : BaseEntity<int>
    {

        /// <summary>
        /// شماره خبری که این کامنت مربوط به آن است
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        /// متن کامنت مورد نظر
        /// </summary>
        public string Text { get; set; }


        /// <summary>
        /// زمانی که کامنت مورد نظر قرار داده شده است
        /// </summary>
        public DateTime Date { get; set; }


        [ForeignKey(nameof(NewsId))]
        public virtual News News { get; set; }
    }
}
