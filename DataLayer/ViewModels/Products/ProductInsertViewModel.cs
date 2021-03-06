﻿using DataLayer.SSOT;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModels.Products
{
    public class ProductInsertViewModel
    {

        public ProductInsertViewModel()
        {
            CreateDate = DateTime.Now;
        }

        [StringLength(150, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        [Required]
        public string Title { get; set; }

        [Required]
        public int ProductGroupId { get; set; }

        [StringLength(2000, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        /// <summary>
        /// توضیح مختصر محصول
        /// </summary>
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "لطفا توضیح کامل محصول را وارد کنید")]
        [DataType(DataType.MultilineText)]
        /// <summary>
        /// توضیح کلی محصول
        /// </summary>
        public string Text { get; set; }

        [Required(ErrorMessage = "لطفا مبلغ را وارد نمایید")]
        public long Price { get; set; }

        /// <summary>
        /// تصویر پیش فرض
        /// </summary>
        public string IndexPic { get; set; }


        /// <summary>
        /// تگ
        /// </summary>
        public string Tags { get; set; }


        /// <summary>
        /// کلید واژه های جستجو
        /// </summary>
        public string SearchKeyWord { get; set; }


        /// <summary>
        /// واحد اندازه گیری محصول
        /// </summary>
        public int? ProductUnitId { get; set; }

        public bool? IsActive { get; set; } = true;

        /// <summary>
        /// تاریخ ثبت محصول
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// وزن محصول
        /// </summary>
        public double? ProductSize { get; set; }

        
    }
}
