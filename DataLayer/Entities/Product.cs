﻿using DataLayer.Entities.Common;
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





        #region Join

        [ForeignKey(nameof(ProductGroupId))]
        public virtual ProductGroup ProductGroup { get; set; }

        #endregion
    }
}
