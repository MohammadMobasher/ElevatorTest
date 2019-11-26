using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entities
{
    public class SlideShow : BaseEntity<int>
    {

        /// <summary>
        /// عنوان مربوط به هر شرط
        /// </summary>
        [StringLength(300, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        [Required]
        public string ImgAddress { get; set; }


        /// <summary>
        /// فعال یا غیر فعال بودن یک آیتم
        /// </summary>
        [Required]
        public bool IsActive { get; set; } = true;



        /// <summary>
        /// از این فیلد برای ذخیره سازی آدرس مربوط به این آیتم می‌باشد
        /// زمانی که کاربر روی این آیتم کلیک کرد به کدام صفحه برود
        /// </summary>
        [StringLength(400, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        public string URL { get; set; }


        /// <summary>
        /// از این فیلد برای seo استفاده می شود
        /// </summary>
        [StringLength(100, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        public string Title { get; set; }


        /// <summary>
        /// این فیلد برای زمانی کاربرد دارد که اگر عکس لود نشد چه متنی نمایش بدهد
        /// </summary>
        [StringLength(200, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        public string Alt { get; set; }
    }
}
