using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Feature
{
    public class FeatureQuestionsViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// عنوان ویژگی (فارسی)
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// آیا در فرم پکیج به عنوان سوال از این ویژگی استفاده بشود
        /// </summary>
        public bool IsQuestion { get; set; }

        /// <summary>
        ///  عنوان سوال آن
        /// </summary>
        public string QuestionTitle { get; set; }

    }
}
