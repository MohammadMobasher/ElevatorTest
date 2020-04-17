using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO.ProductGroupDependencies
{
    public class ProductGroupDependenciesFullDTO
    {
        public int Id { get; set; }

        /// <summary>
        /// شرط مورد نظر
        /// </summary>
        public int? ConditionId { get; set; }
        public string ConditionTitle { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// نام وابستگی
        /// </summary>
        public string Title { get; set; }


        #region گروهی که وابسته است
        public int? GroupId1 { get; set; }
        public string GroupId1Title { get; set; }
        public int? Feature1 { get; set; }
        public string Feature1Title { get; set; }
        public string Value1 { get; set; }
        public string FeatureValueSelected { get; set; }
        #endregion


        #region گروهی که وابستگی به آن است
        public int GroupId2 { get; set; }
        public string GroupId2Title { get; set; }
        public int Feature2 { get; set; }
        public string Feature2Title { get; set; }
        public string Value2 { get; set; }
        #endregion
    }
}
