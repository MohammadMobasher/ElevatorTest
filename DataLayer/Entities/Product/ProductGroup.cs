using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    /// <summary>
    /// محل ذخیره سازی انواع گروه‌ها برای انواع کالا
    /// </summary>
    public class ProductGroup : BaseEntity<int>
    {
        /// <summary>
        /// عنوان مربوط به هر گروه
        /// </summary>
        [StringLength(40, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        [Required]
        public string Title { get; set; }

        
        public int? ParentId { get; set; } = -1;

        public virtual ICollection<Product> Products { get; set; }


        public virtual ProductGroup Parent { get; set; }
        
    }
}
