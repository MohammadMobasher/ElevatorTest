using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities.Features
{
    public class FeatureQuestionForPakage : BaseEntity<int>
    {
        /// <summary>
        /// شماره ویژگی
        /// </summary>
        [Required]
        public int FeatureId { get; set; }

        /// <summary>
        /// شماره گروه
        /// که این ویژگی‌باید برای آن گروه پرسیده شود
        /// </summary>
        [Required]
        public int GroupId { get; set; }

        /// <summary>
        /// متن سوال
        /// </summary>
        [StringLength(150)]
        public string QuestionTitle { get; set; }

        #region relation

        [ForeignKey(nameof(FeatureId))]
        public virtual Feature Feature { get; set; }


        [ForeignKey(nameof(GroupId))]
        public virtual ProductGroup ProductGroup { get; set; }

        #endregion
    }
}
