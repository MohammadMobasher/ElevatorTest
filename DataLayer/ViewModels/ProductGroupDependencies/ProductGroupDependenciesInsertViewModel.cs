using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.ProductGroupDependencies
{
    public class ProductGroupDependenciesInsertViewModel
    {

        /// <summary>
        /// شرط مورد نظر
        /// </summary>
        public int? ConditionId { get; set; }

        /// <summary>
        /// نام وابستگی
        /// </summary>
        public string Title { get; set; }


        #region گروهی که وابسته است
        public int? GroupId1 { get; set; }
        public int? Feature1 { get; set; }
        public string Value1 { get; set; }
        #endregion


        #region گروهی که وابستگی به آن است
        public int? GroupId2 { get; set; }
        public int? Feature2 { get; set; }
        public string Value2 { get; set; }
        #endregion


    }
}
