using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.DTO.LogoManufactory
{
    public class LogoManufactoryDTO
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string Title { get; set; }


        [StringLength(1000)]
        public string URL { get; set; }


        [StringLength(500)]
        public string AddressImg { get; set; }
    }
}
