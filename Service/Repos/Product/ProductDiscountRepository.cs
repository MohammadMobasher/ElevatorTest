using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Repos.Product
{
    public class ProductDiscountRepository : GenericRepository<ProductDiscount>
    {
        public ProductDiscountRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
