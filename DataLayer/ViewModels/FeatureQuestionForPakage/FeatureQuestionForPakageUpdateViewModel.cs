using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.FeatureQuestionForPakage
{
    public class FeatureQuestionForPakageUpdateViewModel
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public int GroupId { get; set; }
        public string QuestionTitle { get; set; }
    }
}
