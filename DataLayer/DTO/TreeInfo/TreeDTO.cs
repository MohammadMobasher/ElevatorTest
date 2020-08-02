using DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.DTO.TreeInfo
{
    public class TreeDTO
    {
        public int Id { get; set; }
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
        public virtual Users User { get; set; }


        #endregion

    }
}
