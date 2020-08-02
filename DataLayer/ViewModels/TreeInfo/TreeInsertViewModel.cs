using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.TreeInfo
{
    public class TreeInsertViewModel
    {



        /// <summary>
        /// شماره کاربری
        /// </summary>
        public int UserId { get; set; }


        /// <summary>
        /// آیا این رکورد استفاده شده است
        /// </summary>
        public bool IsPlanted { get; set; } = false;

    }
}
