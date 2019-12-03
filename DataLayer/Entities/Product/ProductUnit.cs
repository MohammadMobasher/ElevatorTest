using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entities
{
    /// <summary>
    /// جدول مربوط به واحد‌های مورد استفاده در مورد کالاها
    /// </summary>
    public class ProductUnit : BaseEntity<int>
    {

        /// <summary>
        /// عنوان مربوط به هر واحد
        /// </summary>
        [StringLength(40, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        [Required]
        public string Title { get; set; }


        /// <summary>
        /// علامتی که برای هر واحد به کار گرفته می ‌شود دراین فیلد قرار می‌گیرد
        /// </summary>
        [StringLength(10, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        [Required]
        public string Name { get; set; }

    }
}
