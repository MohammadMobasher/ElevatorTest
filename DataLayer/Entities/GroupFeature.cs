using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class GroupFeature : BaseEntity<int>
    {

        /// <summary>
        /// عنوان ویژگی
        /// </summary>
        [StringLength(150, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        [Required]
        public string Title { get; set; }


        /// <summary>
        /// نام ویژگی
        /// </summary>
        [StringLength(150, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        [Required]
        public string Name { get; set; }


        public int GroupId { get; set; }


        #region Join

        [ForeignKey(nameof(GroupId))]
        public virtual  ProductGroup ProductGroup { get; set; }

        #endregion


    }
}
