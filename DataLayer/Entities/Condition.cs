using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Entities
{
    /// <summary>
    /// شروط مروبوطه در این جدول قرار میگیرد
    /// </summary>
    public class Condition : BaseEntity<int>
    {

        /// <summary>
        /// عنوان مربوط به هر شرط
        /// </summary>
        [StringLength(40, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        [Required]
        public string Title { get; set; }


        /// <summary>
        /// علامتی که برای هر شرط به کار گرفته می ‌شود دراین فیلد قرار میگیرد
        /// </summary>
        [StringLength(10, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        [Required]
        public string Name { get; set; }

    }
}
