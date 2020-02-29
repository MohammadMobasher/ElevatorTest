using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entities
{
    public class LogoManufactory : BaseEntity<int>
    {
        [StringLength(150)]
        public string Title { get; set; }


        [StringLength(1000)]
        public string URL { get; set; }


        [StringLength(500)]
        public string AddressImg { get; set; }

    }
}
