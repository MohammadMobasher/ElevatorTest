﻿using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.SSOT;

namespace DataLayer.ViewModels.Feature {
    public class FeatureQuestionsViewModel {
     

        public int Id { get; set; }
    

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