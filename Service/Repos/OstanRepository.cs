using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Shahr>> GetByOstanId(int ostanId)
        {
            return await this.DbContext.Shahr.Where(x => x.OstanId == ostanId).ToListAsync();
        }
    }
}
