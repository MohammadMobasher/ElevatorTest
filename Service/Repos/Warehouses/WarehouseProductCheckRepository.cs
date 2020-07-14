using AutoMapper.QueryableExtensions;
using DataLayer.DTO.WarehouseProductCheckDTO;
using DataLayer.Entities.Warehouse;
using DataLayer.ViewModels.WarehouseProductChecks;
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
        /// گرفتن تمامی تراکنش های این انبار بر اساس شناسه انبار
        /// </summary>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public async Task<List<WarehouseProductCheckFullDTO>> GetAllTransactionInWarehouse(int warehouse)
            => await TableNoTracking.Where(a => a.WarehouseId == warehouse)
            .ProjectTo<WarehouseProductCheckFullDTO>().ToListAsync();


        public async Task<Tuple<int, List<WarehouseProductCheckFullDTO>>> LoadAsyncCount(
           int warehouseId,
           int skip = -1,
           int take = -1,
           WarehouseProductCheckSearchViewModel model = null)
        {
            var query = Entities.Include(x=> x.Product).ProjectTo<WarehouseProductCheckFullDTO>().Where(x=> x.WarehouseId == warehouseId);

            if (!string.IsNullOrEmpty(model.ProductTitle))
                query = query.Where(x => x.Product.Title.Contains(model.ProductTitle));

            if(model.Count != null)
                query = query.Where(x => x.Count == model.Count);

            if(model.Type != null)
                query = query.Where(x => x.TypeSSOt == model.Type);


            //if (model.Id != null)
            //    query = query.Where(x => x.Id == model.Id);

            //if (!string.IsNullOrEmpty(model.Title))
            //    query = query.Where(x => x.Title.Contains(model.Title));

            //if (model.ParentId != null)
            //    query = query.Where(x => x.ParentId == model.ParentId);

            int Count = query.Count();

            query = query.OrderByDescending(x => x.Id);


            if (skip != -1)
                query = query.Skip((skip - 1) * take);

            if (take != -1)
                query = query.Take(take);

            return new Tuple<int, List<WarehouseProductCheckFullDTO>>(Count, await query.ToListAsync());
        }
    }
}
