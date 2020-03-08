using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Repos
{
    public class ShopProductRepository : GenericRepository<ShopProduct>
    {
        public ShopProductRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
