using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    /// <summary>
    /// جدول پاسخ به سوالات پکیج
    /// </summary>
    public class PackageUserAnswers:BaseEntity<int>
    {
        public int FeatureId { get; set; }

        public int Answer{ get; set; }

        public int UserId { get; set; }

        public bool IsManager { get; set; }
    }
}
