using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entities
{
    public class StoreRoom : BaseEntity<int>
    {
        /// <summary>
        /// عنوان انبار
        /// </summary>
        [StringLength(100, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        [Required]
        public string Title { get; set; }


        /// <summary>
        /// آدرس مربوط به انبار
        /// </summary>
        [StringLength(200, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        public string Address { get; set; }

    }
}
