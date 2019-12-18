using Core.Utilities;
using DataLayer.Entities;
using DataLayer.ViewModels.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos.Product
{
    public class ProductFeatureRepository : GenericRepository<ProductFeature>
    {
        public ProductFeatureRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// افزودن گروهی ویژگی ها
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public async Task<SweetAlertExtenstion> AddFeatureRange(ProductFeatureInsertViewModel vm)
        {
            var lst = new List<ProductFeature>();

            foreach (var item in vm.Items)
            {
                lst.Add(new ProductFeature()
                {
                    FeatureId = item.FeatureId,
                    FeatureValue = item.FeatureValue,
                    ProductId = vm.ProductId
                });
            }
            await AddRangeAsync(lst);
            return SweetAlertExtenstion.Ok();
        }

        /// <summary>
        /// افزودن گروهی ویژگی ها
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public async Task<SweetAlertExtenstion> UpdateFeatureRange(ProductFeatureInsertViewModel vm)
        {
            DeleteFeatures(vm.ProductId);

            var lst = new List<ProductFeature>();

            foreach (var item in vm.Items)
            {
                lst.Add(new ProductFeature()
                {
                    FeatureId = item.FeatureId,
                    FeatureValue = item.FeatureValue,
                    ProductId = vm.ProductId
                });
            }
            await AddRangeAsync(lst);
            return SweetAlertExtenstion.Ok();
        }

        void DeleteFeatures(int id)
        {
            var model = TableNoTracking.Where(a => a.ProductId == id).ToList();

            DbContext.RemoveRange(model);
        }

        /// <summary>
        /// گرفتن اطلاعات ویژگی های محصولات بر اساس شناسه محصولات
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<List<ProductFeature>> GetAllProductFeatureByProductId(int productId)
            => await TableNoTracking.Where(a => a.ProductId == productId).ToListAsync();
    }
}
