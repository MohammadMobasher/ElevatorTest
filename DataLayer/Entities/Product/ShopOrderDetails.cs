using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class ShopOrderDetails : BaseEntity
    {
        public int UserId { get; set; }

        public int? OrderId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string CompanyName { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string Address { get; set; }


        [ForeignKey(nameof(UserId))]
        public virtual Users.Users Users { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual ShopOrder ShopOrder { get; set; }
    }
}
