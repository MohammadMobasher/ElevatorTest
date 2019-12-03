using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class ProductDependencies : BaseEntity<int>
    {

        /// <summary>
        /// شماره کالا
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// شماره وابستگی مورد نظر
        /// </summary>
        public int DependencyId { get; set; }




    }
}
