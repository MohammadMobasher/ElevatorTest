using AutoMapper.QueryableExtensions;
using DataLayer.DTO.WarehouseProductCheckDTO;
using DataLayer.Entities.Warehouse;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos.Warehouses
{
    public class WarehouseProductCheckRepository : GenericRepository<WarehouseProductCheck>
    {
        public WarehouseProductCheckRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// گرفتن تمامی تراکنش های این انبار بر اساس شناس هانبار
        /// </summary>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public async Task<List<WarehouseProductCheckFullDTO>> GetAllTransactionInWarehouse(int warehouse)
            => await TableNoTracking.Where(a => a.WarehouseId == warehouse)
            .ProjectTo<WarehouseProductCheckFullDTO>().ToListAsync();



    }
}
