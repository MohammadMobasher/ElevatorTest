using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace DataLayer.Entities
{
    public class Favorites :BaseEntity<int>
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }


        public DateTime SubmitDate { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual Entities.Users.Users User { get; set; }


        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

    }
}
