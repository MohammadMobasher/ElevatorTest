using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class ProductGroupDependencies : BaseEntity<int>
    {

        /// <summary>
        /// عنوان وابستگی
        /// </summary>
        [StringLength(300, ErrorMessage = "متن وارد شده بیشتر از حد مجاز است")]
        [Required]
        public string Title { get; set; }


        #region گروهی که وابسته است
        public int GroupId1 { get; set; }
        public int Feature1 { get; set; }
        public string Value1 { get; set; }
        #endregion


        #region گروهی که وابستگی به آن است
        public int GroupId2 { get; set; }
        public int Feature2 { get; set; }
        public string Value2 { get; set; }
        #endregion


        /// <summary>
        /// شرط بین این دو برای وابستگی
        /// </summary>
        public int ConditionId { get; set; }
        
        #region Join

        //[ForeignKey(nameof(GroupId1))]
        //public virtual ProductGroup ProductGroup1 { get; set; }

        //[ForeignKey(nameof(GroupId2))]
        //public virtual ProductGroup ProductGroup2 { get; set; }

        [ForeignKey(nameof(ConditionId))]
        public virtual Condition Condition { get; set; }

        //[ForeignKey(nameof(Feature1))]
        //public virtual GroupFeature GroupFeature1 { get; set; }

        //[ForeignKey(nameof(Feature2))]
        //public virtual GroupFeature GroupFeature2 { get; set; }


        #endregion

        
    }
}
