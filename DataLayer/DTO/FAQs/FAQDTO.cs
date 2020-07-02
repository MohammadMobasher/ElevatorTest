using DataLayer.BaseClasses;
using DataLayer.Entities.FAQs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.DTO.FAQs
{
    public class FAQDTO : BaseMapping<FAQDTO, FAQ, int>
    {
        //public int Id { get; set; }

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
        public bool IsActive { get; set; }


        public int FaqGroupId { get; set; }

        #region Relation

        [ForeignKey(nameof(FaqGroupId))]
        public virtual FaqGroup FaqGroup { get; set; }

        #endregion

    }
}
