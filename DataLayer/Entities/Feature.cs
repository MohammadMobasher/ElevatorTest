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


        ///// <summary>
        ///// نام ویژگی (انگیلسی)
        ///// </summary>
        //[StringLength(150, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        //[Required]
        //public string Name { get; set; }


        /// <summary>
        /// نوع این ویژگی
        /// </summary>
        [Required]
        public FeatureTypeSSOT FeatureType { get; set; }

        /// <summary>
        /// در این فیلد زمانی که نوع فیلد به صورت 
        /// ssot
        /// باشد، مقادیر مربوط به آن قرار میگیرد
        /// </summary>
        public string SSOTValue { get; set; }
    }
}
