using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.ShopProduct
{
    public class ShopProductAddPackageViewModel
    {
        public ShopProductAddPackageViewModel()
        {
            RequestedDate=DateTime.Now;
            IsFinaly = false;
        }
        public int PackageId { get; set; }

        public int UserId { get; set; }

        public int Count { get; set; }

        public DateTime RequestedDate { get; }

        public bool IsFinaly { get;  }
    }
}
