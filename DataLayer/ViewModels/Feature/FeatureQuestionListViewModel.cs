using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Feature
{
    public class FeatureQuestionListViewModel
    {
        public FeatureQuestionViewModel Question { get; set; }
        public List<FeatureQuestionListItemViewModel> Items { get; set; }
    }

    public class FeatureQuestionViewModel
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public string QuestionTitle { get; set; }

        public int GroupId { get; set; }
    }

    public class FeatureQuestionListItemViewModel
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public int FeatureId { get; set; }
    }
}
