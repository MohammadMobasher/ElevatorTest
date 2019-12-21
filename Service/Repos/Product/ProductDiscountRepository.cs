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


        /// <summary>
        /// آیا این تخفیف برای این محصول قبلا ثبت شده است یا خیر 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> IsProductSubmited(int id)
            => await GetByConditionAsync(a=>a.ProductId == id) != null;

        /// <summary>
        /// آیا این تخفیف برای این گروه محصول قبلا ثبت شده است یا خیر 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> IsProductGroupSubmited(int id)
            => await GetByConditionAsync(a => a.ProductGroupId == id) != null;


        public async Task UpdateDiscount(ProductDiscountUpdateViewModel vm)
        {
            var model =await GetByIdAsync(vm.Id);

            Mapper.Map(vm, model);

            await DbContext.SaveChangesAsync();
        }

    }
}
