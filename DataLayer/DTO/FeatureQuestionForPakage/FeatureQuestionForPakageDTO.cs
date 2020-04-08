using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO.FeatureQuestionForPakage
{
    public class FeatureQuestionForPakageDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// شماره ویژگی
        /// </summary>

        public int FeatureId { get; set; } = -1;

        /// <summary>
        /// شماره گروه
        /// که این ویژگی‌باید برای آن گروه پرسیده شود
        /// </summary>
        
        public int GroupId { get; set; } = -1;

        /// <summary>
        /// متن سوال
        /// </summary>
        public string QuestionTitle { get; set; }

    }
}
