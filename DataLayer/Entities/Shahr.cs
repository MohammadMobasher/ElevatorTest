using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class Shahr : BaseEntity<int>
    {
        public int OstanId { get; set; }
        public string Name { get; set; }


        [ForeignKey(nameof(OstanId))]
        public virtual Ostan Ostan { get; set; }
    }
}
