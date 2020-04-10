using DataLayer.Entities.Common;
using DataLayer.Entities.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    /// <summary>
    /// جدول پاسخ به سوالات پکیج
    /// </summary>
    public class PackageUserAnswers : BaseEntity<int>
    {
        public int QuestionId { get; set; }

        public int FeatureId { get; set; }

        public string Answer { get; set; }

        public int UserId { get; set; }

        public bool IsManager { get; set; }

        public int PackageId { get; set; }

        [ForeignKey(nameof(QuestionId))]

        public virtual FeatureQuestionForPakage FeatureQuestionForPakage { get; set; }

        [ForeignKey(nameof(PackageId))]
        public virtual ProductPackage ProductPackage { get; set; }

        [ForeignKey(nameof(FeatureId))]
        public virtual Feature Features { get; set; }


    }
}
