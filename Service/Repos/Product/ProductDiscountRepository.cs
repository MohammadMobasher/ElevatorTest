using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Utilities;
using DataLayer.DTO.ProductDiscounts;
using DataLayer.Entities;
using DataLayer.ViewModels.ProductDiscount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos.Product
{
    public class ProductDiscountRepository : GenericRepository<ProductDiscount>
    {
        public ProductDiscountRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }


        public async Task<List<ProductDiscountDTO>> DiscountToAll(Expression<Func<ProductDiscount, bool>> where = null
            , Func<IQueryable<ProductDiscount>, IOrderedQueryable<ProductDiscount>> orderBy = null)
                => await TableNoTracking.WhereIf(where != null, where).OrderByIf(orderBy != null, orderBy)
                         .ProjectTo<ProductDiscountDTO>().ToListAsync();

        public async Task UpdateDiscount(int id, ProductDiscountInsertViewModel vm)
        {
            var model =await GetByIdAsync(id);

            Mapper.Map(vm, model);

           await DbContext.SaveChangesAsync();
        }
    }
}
