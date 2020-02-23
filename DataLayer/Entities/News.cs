using DataLayer.Entities.Common;
using DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    /// <summary>
    /// جدول اخبار
    /// </summary>
    public class News : BaseEntity<int>
    {

        /// <summary>
        /// عنوان مربوط به هر خبر
        /// </summary>
        [StringLength(200, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        [Required]
        public string Title { get; set; }


        /// <summary>
        /// خلاصه خبر
        /// </summary>
        [StringLength(600, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        [Required]
        public string SummeryNews { get; set; }

        /// <summary>
        /// توضیحات خبر
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// زمان ثبت خبر
        /// </summary>
        [Required]
        public DateTime Date { get; set; }


        /// <summary>
        /// تعداد بازدید مربو به این خبر
        /// </summary>
        public int ViewCount { get; set; } = 0;



        /// <summary>
        /// تگ‌های مربوط به هر خبر
        /// </summary>
        [StringLength(400, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        public string Tags { get; set; }
        
        /// <summary>
        /// یک خبر فعال هست یا خیر
        /// </summary>
        public bool IsActive { get; set; }


        public int UserId { get; set; }



        [Required]
        [StringLength(1000, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        public string ImageAddress { get; set; }

        [Required]
        public int NewsGroupId { get; set; }




        #region UserId

        [ForeignKey(nameof(UserId))]
        public virtual Users.Users Users { get; set; }

        [ForeignKey(nameof(NewsGroupId))]
        public virtual NewsGroup NewsGroup { get; set; }

        #endregion
    }
}
