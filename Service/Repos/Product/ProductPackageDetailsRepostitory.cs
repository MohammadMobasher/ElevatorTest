using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Utilities;
using DataLayer.DTO.Feature;
using DataLayer.DTO.FeatureItem;
using DataLayer.DTO.Products;
using DataLayer.ViewModels.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Service.Repos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos
{
    public class ProductPackageDetailsRepostitory : GenericRepository<DataLayer.Entities.ProductPackageDetails>
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ProductDiscountRepository _productDiscountRepository;
        private readonly ProductRepostitory _productRepostitory;

        public ProductPackageDetailsRepostitory(DatabaseContext dbContext,
            IHostingEnvironment hostingEnvironment,
            ProductDiscountRepository productDiscountRepository, ProductRepostitory productRepostitory) : base(dbContext)
        {
            _hostingEnvironment = hostingEnvironment;
            _productDiscountRepository = productDiscountRepository;
            _productRepostitory = productRepostitory;
        }


        /// <summary>
        /// محاسبه قیمت کلی محصول
        /// </summary>
        /// <param name="packageId"></param>
        /// <returns></returns>
        public async Task<string> CalculatePrice(int packageId)
        {
            try
            {
                var model = await TableNoTracking.Include(a => a.Product).Where(a => a.PackageId == packageId)
                        .SumAsync(a => a.Product.Price);

                return model.ToString("n0");
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool> IsExist(int packageId, int productId)
         =>await TableNoTracking.AnyAsync(a => a.ProductId == productId && a.PackageId == packageId);


        public async Task<long> ResultPrice(int packageId)
        {
            var model = await TableNoTracking
                .Where(a => a.PackageId == packageId)
                .Include(a=>a.Product)
                .ToListAsync();

            var sum = default(long);

            foreach (var item in model)
            {
                sum += await _productRepostitory.ResultPrice(item.ProductId);
            }

            return sum;
        }
    }
}
