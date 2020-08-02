using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities.TreeInfo
{
    public class Tree : BaseEntity<int>
    {
        /// <summary>
        /// شماره کاربری
        /// </summary>
        public int UserId { get; set; }


        /// <summary>
        /// آیا این رکورد استفاده شده است
        /// </summary>
        public bool IsPlanted { get; set; } = false;


        public string Address { get; set; }

        public int TreeNumber { get; set; }


        #region Relation


        [ForeignKey(nameof(UserId))]
        public virtual Users.Users User { get; set; }


        #endregion

    }
}
