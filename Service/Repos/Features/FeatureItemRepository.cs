using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Repos
{
    public class FeatureItemRepository : GenericRepository<FeatureItem>
    {
        public FeatureItemRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
