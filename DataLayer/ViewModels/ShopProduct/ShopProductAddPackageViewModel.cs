using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.ShopProduct
{
    public class ShopProductAddPackageViewModel
    {
        public ShopProductAddPackageViewModel()
        {
            Count = 1;
            RequestedDate=DateTime.Now;
            IsFinaly = false;
        }
        public int PackageId { get; set; }

        public int UserId { get; set; }

        public int Count { get;  }

        public DateTime RequestedDate { get; }

        public bool IsFinaly { get;  }
    }
}
