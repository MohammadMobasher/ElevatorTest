using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Repos
{
    public class ProductRepostitory : GenericRepository<DataLayer.Entities.Product>
    {
        public ProductRepostitory(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
