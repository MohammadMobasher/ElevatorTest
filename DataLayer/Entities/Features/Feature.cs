﻿using DataLayer.Entities.Common;
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

        public bool ShowForSearch { get; set; } = false;



        /// <summary>
        /// آیا در فرم پکیج به عنوان سوال از این ویژگی استفاده بشود
        /// </summary>
        public bool IsQuestion { get; set; }

        /// <summary>
        ///  عنوان سوال آن
        /// </summary>
        public string QuestionTitle { get; set; }

        /// <summary>
        /// دسترسی مستقیم به داده های جدول
        /// </summary>
        public virtual ICollection<FeatureItem> Features { get; set; }
    }
}
