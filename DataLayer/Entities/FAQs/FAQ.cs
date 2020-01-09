using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities.FAQs
{
    public class FAQ : BaseEntity<int>
    {
        /// <summary>
        /// متن مربوط به سوال
        /// </summary>
        public string QuestionText { get; set; }
        /// <summary>
        /// متن مربوط به جواب
        /// </summary>
        public string AnswerText { get; set; }
        /// <summary>
        /// فعال یا غیر فعال بودن یک سوال
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// شماره گروه مربوط به سوالات پرتکرار
        /// </summary>
        [Required]
        public int FaqGroupId { get; set; }


        #region Relation

        [ForeignKey(nameof(FaqGroupId))]
        public virtual FaqGroup FaqGroup { get; set; }

        #endregion

    }
}
