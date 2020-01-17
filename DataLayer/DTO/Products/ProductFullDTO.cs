using DataLayer.Entities;
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

        /// <summary>
        /// توضیح کلی محصول
        /// </summary>
        public string Text { get; set; }

        public long Price { get; set; }

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

        public bool? IsActive { get; set; }

        /// <summary>
        /// تاریخ ثبت محصول
        /// </summary>
        public DateTime? CreateDate { get; set; }
        #region Join

        public virtual ProductGroup ProductGroup { get; set; }

        public virtual DataLayer.Entities.ProductUnit ProductUnit { get; set; }
        #endregion
    }
}
