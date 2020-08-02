using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities.TreeInfo
{
    public class UserTreeReminded : BaseEntity<int>
    {

        public int UserId { get; set; }

        public double Reminded { get; set; }


        #region Relation


        [ForeignKey(nameof(UserId))]
        public virtual Users.Users User { get; set; }


        #endregion

    }
}
