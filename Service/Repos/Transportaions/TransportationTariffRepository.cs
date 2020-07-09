using DataLayer.Entities.Transportation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Repos.Transportaions
{
    public class TransportationTariffRepository : GenericRepository<TransportationTariff>
    {
        public TransportationTariffRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
