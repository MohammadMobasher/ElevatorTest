using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class FactorItem : BaseEntity<int>
    {

        /// <summary>
        /// شماره کالای که خریداری شده است
        /// </summary>
        public int ProductId { get; set; }


        /// <summary>
        /// مبلغ کالا
        /// </summary>
        public int Amount { get; set; }


        /// <summary>
        /// تعداد خریداری شده از هر کالا
        /// </summary>
        public int Number { get; set; }


        /// <summary>
        /// شماره فاکتور مربوطه که این کالا در آن خریداری شده است
        /// </summary>
        public int FactorId { get; set; }






        #region Join

        [ForeignKey(nameof(FactorId))]
        public virtual FactorAndPackage Factor { get; set; }


        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        #endregion
    }
}
