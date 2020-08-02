using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.TreeInfo
{
    public class TreeUpdateViewModel
    {
        public int Id { get; set; }


        /// <summary>
        /// آیا این رکورد استفاده شده است
        /// </summary>
        public bool IsPlanted { get; set; }


        public string Address { get; set; }

        public int TreeNumber { get; set; }
    }
}
