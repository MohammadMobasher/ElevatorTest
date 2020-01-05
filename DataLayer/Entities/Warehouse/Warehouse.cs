using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities.Warehouse
{
    public class Warehouse : BaseEntity<int>
    {
      

        /// <summary>
        /// عنوان
        /// </summary>
        public string Title{ get; set; }


        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// منطقه
        /// </summary>
        public int Region { get; set; }

        public string Address{ get; set; }

        public string PhoneNumber { get; set; }

        public bool IsActivity { get; set; }

        
    }
}
