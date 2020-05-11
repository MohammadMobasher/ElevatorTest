using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Repos
{
    public class ShopOrderRepository : GenericRepository<ShopOrder>
    {
        public ShopOrderRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }


    }
}
