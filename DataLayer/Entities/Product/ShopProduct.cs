using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class ShopProduct:BaseEntity<int>
    {
        public int ProductId { get; set; }

        public int UserId { get; set; }

        public int Count { get; set; }

        public DateTime RequestedDate { get; set; }

        public bool IsFinaly { get; set; }
    }
}
