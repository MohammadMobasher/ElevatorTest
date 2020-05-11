using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class ShopOrder : BaseEntity
    {
        public DateTime? CreateDate { get; set; }

        public long Amount { get; set; }

        public bool IsSuccessed { get; set; }

        public DateTime SuccessDate { get; set; }

        public int UserId { get; set; }

        public string DiscountCode { get; set; }

        public int? OrderId { get; set; }



        [ForeignKey(nameof(UserId))]
        public virtual Users.Users Users { get; set; }



    }
}
