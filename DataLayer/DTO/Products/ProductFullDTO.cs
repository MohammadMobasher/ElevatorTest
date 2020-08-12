using DataLayer.Entities;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO.Products
{
    public class ProductFullDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int ProductGroupId { get; set; }

        /// <summary>
        /// توضیح مختصر محصول
        /// </summary>
        public string ShortDescription { get; set; }

        public int CountExist { get; set; }

        /// <summary>
        /// توضیح کلی محصول
        /// </summary>
        public string Text { get; set; }

        public long Price { get; set; }

        public long PriceWithDiscount { get; set; }


        public DateTime? DiscountEndDate { get; set; }

        public DateTime? DiscountStartDate { get; set; }


        /// <summary>
        /// تصویر پیش فرض
        /// </summary>
        public string IndexPic { get; set; }

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
        /// کلید واژه های جستجو
        /// </summary>
        public string SearchKeyWord { get; set; }

        public bool? IsActive { get; set; }
        /// <summary>
        /// آیا این محصول جز فروش ویژه است؟
        /// </summary>
        public bool IsSpecialSell { get; set; }

        /// <summary>
        /// تاریخ ثبت محصول
        /// </summary>
        public DateTime? CreateDate { get; set; }

        public int NewProductGroupId { get; set; }

        /// <summary>
        /// وزن محصول
        /// </summary>
        public double? ProductSize { get; set; }

        /// <summary>
        /// آیا محصول موجود می باشد یا خیر
        /// ComputedField !!
        /// </summary>
        public bool IsExist { get; set; }

        #region Join

        public virtual ProductGroup ProductGroup { get; set; }

        public virtual Entities.ProductUnit ProductUnit { get; set; }

        public virtual ProductDiscount ProductDiscount { get; set; }

        public virtual ICollection<ProductFeature> Features { get; set; }
        #endregion
    }
}
