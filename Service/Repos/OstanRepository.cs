using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos
{
    public class OstanRepository : GenericRepository<Ostan>
    {
        public OstanRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Ostan>> GetAll()
        {
            return await this.Entities.AsNoTracking().ToListAsync();
        }
    }
}
