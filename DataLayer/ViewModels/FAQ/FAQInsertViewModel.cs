using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.FAQ
{
    public class FAQInsertViewModel
    {
        public string QuestionText { get; set; }
        /// <summary>
        /// متن مربوط به جواب
        /// </summary>
        public string AnswerText { get; set; }

        public int FaqGroupId { get; set; }
        /// <summary>
        /// فعال یا غیر فعال بودن یک سوال
        /// </summary>
        public bool IsActive { get; set; } 
    }
}
