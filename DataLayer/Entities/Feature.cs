using DataLayer.Entities.Common;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entities
{
    public class Feature : BaseEntity<int>
    {
        /// <summary>
        /// عنوان ویژگی (فارسی)
        /// </summary>
        [StringLength(150, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        [Required]
        public string Title { get; set; }
        

        /// <summary>
        /// نوع این ویژگی
        /// </summary>
        [Required]
        public FeatureTypeSSOT FeatureType { get; set; }


        /// <summary>
        /// آیا این فیلد به صورت ضروری است و باید پر شود
        /// </summary>
        public bool IsRequired { get; set; } = false;
    }
}
