using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entities
{
    public class SuggestionsAndComplaint : BaseEntity<int>
    {

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Family { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }

    }
}
