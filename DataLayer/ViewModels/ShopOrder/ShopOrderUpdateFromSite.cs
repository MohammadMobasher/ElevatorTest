using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.ShopOrder
{
    public class ShopOrderUpdateFromSite
    {
        /// <summary>
        /// شماره فاکتور مورد نظر
        /// </summary>
        public int ShopOrderId { get; set; }

        public List<ListProductShopOrderUpdateFromSite> ListProducts { get; set; }

    }


    public class ListProductShopOrderUpdateFromSite
    {
        public int ProductId { get; set; }

        public int Count { get; set; }
    }
}
