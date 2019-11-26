using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class NewsGroupItem : BaseEntity<int>
    {
        /// <summary>
        /// شماره گروه
        /// </summary>
        public int NewsGroupId { get; set; }


        /// <summary>
        /// شماره خبر
        /// </summary>
        public int NewsId { get; set; }


        #region Join

        [ForeignKey(nameof(NewsGroupId))]
        public virtual NewsGroup NewsGroup { get; set; }


        [ForeignKey(nameof(NewsId))]
        public virtual News News { get; set; }

        #endregion

    }
}
