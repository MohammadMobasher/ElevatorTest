using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.ShopProduct
{
    public class ShopProductAddViewModel
    {
        public ShopProductAddViewModel()
        {
            Count = 1;
            RequestedDate=DateTime.Now;
            IsFinaly = false;
        }
        public int ProductId { get; set; }

        public int UserId { get; set; }

        public int Count { get;  }

        public DateTime RequestedDate { get; }

        public bool IsFinaly { get;  }
    }
}
