using DataLayer.Entities.Common;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class Product : BaseEntity<int>
    {
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
        
        
        [DataType(DataType.MultilineText)]
        /// <summary>
        /// توضیح کلی محصول
        /// </summary>
        public string Text { get; set; }

        [Required(ErrorMessage = "لطفا مبلغ را وارد نمایید")]
        public long Price { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long PriceWithDiscount { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DiscountEndDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DiscountStartDate { get; set; }

        /// <summary>
        /// تصویر پیش فرض
        /// </summary>
        public string IndexPic{ get; set; }

        /// <summary>
        /// تعداد لایک ها
        /// </summary>
        public int Like { get; set; }

        /// <summary>
        /// تعداد دیس لایک ها
        /// </summary>
        public int DisLike { get; set; }

        /// <summary>
        /// تعداد بازدید
        /// </summary>
        public int Visit { get; set; }

        /// <summary>
        /// واحد اندازه گیری محصول
        /// </summary>
        public int? ProductUnitId { get; set; }

        /// <summary>
        /// تگ
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// تاریخ ثبت محصول
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// آیا این محصول جز فروش ویژه است؟
        /// </summary>
        public bool IsSpecialSell { get; set; }

        /// <summary>
        /// فعال  / غیرفعال
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// وزن محصول
        /// </summary>
        public double? ProductSize { get; set; }

        /// <summary>
        /// آیا محصول موجود می باشد یا خیر
        /// ComputedField !!
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool IsExist { get; set; }

        /// <summary>
        /// آیا این محصول حذف شده است یا خیر
        /// </summary>
        public bool IsDeleted { get; set; } = false;


        #region Join

        [ForeignKey(nameof(ProductGroupId))]
        public virtual ProductGroup ProductGroup { get; set; }

        [ForeignKey(nameof(ProductUnitId))]
        public virtual ProductUnit ProductUnit { get; set; }

        public virtual ICollection<ProductFeature> ProductFeatures { get; set; }
        #endregion
    }
}
