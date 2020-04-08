using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.FeatureQuestionForPakage
{
    public class FeatureQuestionForPakageSearchViewModel
    {
        /// <summary>
        /// شماره ویژگی
        /// </summary>

        public int FeatureId { get; set; }

        /// <summary>
        /// شماره گروه
        /// که این ویژگی‌باید برای آن گروه پرسیده شود
        /// </summary>

        public int GroupId { get; set; }

        /// <summary>
        /// متن سوال
        /// </summary>
        public string QuestionTitle { get; set; }
    }
}
