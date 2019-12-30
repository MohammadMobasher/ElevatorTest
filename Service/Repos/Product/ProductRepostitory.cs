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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos
{
    public class ProductRepostitory : GenericRepository<DataLayer.Entities.Product>
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public ProductRepostitory(DatabaseContext dbContext,IHostingEnvironment hostingEnvironment) : base(dbContext)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        public async Task<Tuple<int, List<ProductFullDTO>>> LoadAsyncCount(
         int skip = -1,
         int take = -1,
         ProductSearchViewModel model = null)
        {
            var query = Entities.ProjectTo<ProductFullDTO>();


            if (model.Id != null)
                query = query.Where(x => x.Id == model.Id);

            if (!string.IsNullOrEmpty(model.Title))
                query = query.Where(x => x.Title.Contains(model.Title));

            if(model.Price != null)
            {
                query = query.Where(x => x.Price == model.Price);
            }

            if (model.ProductGroupId != null)
            {
                query = query.Where(x => x.ProductGroupId == model.ProductGroupId.Value);
            }

            if (model.Likes != null)
            {
                query = query.Where(x => (x.Like - x.DisLike) == model.Likes.Value);
            }


            int Count = query.Count();

            query = query.OrderByDescending(x => x.Id);


            if (skip != -1)
                query = query.Skip((skip - 1) * take);

            if (take != -1)
                query = query.Take(take);

            return new Tuple<int, List<ProductFullDTO>>(Count, await query.ToListAsync());
        }


        /// <summary>
        /// گرفتن اطلاعات گروه محصول بر اساس شناسه محصول
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int?> GetProductGroupIdbyProductId(int id)
        {
            var product = await TableNoTracking.FirstOrDefaultAsync(a => a.Id == id);

            if (product == null) return null;

            return product.ProductGroupId;
        }

        public async Task<int> SubmitProduct(DataLayer.ViewModels.Products.ProductInsertViewModel vm,IFormFile file)
        {
            vm.IndexPic = MFile.Save(file, FilePath.Product.GetDescription());

            var mapModel = Map(vm);

            await AddAsync(mapModel);

            return mapModel.Id;
        }

        public async Task<int> UpdateProduct(DataLayer.ViewModels.Products.ProductUpdateViewModel vm, IFormFile file)
        {
            if (file != null)
            {
                if (vm.IndexPic != null)
                {
                    var WebContent = _hostingEnvironment.WebRootPath;

                    System.IO.File.Delete(WebContent + FilePath.Product.GetDescription());
                }

                vm.IndexPic = MFile.Save(file, FilePath.Product.GetDescription());
            }

            var model = GetById(vm.Id);

            Mapper.Map(vm, model);

            DbContext.SaveChanges();

            return model.Id;
        }


        /// در این تابع لیست ویژگی‌هایی برگردانده می‌شود که متعلق به یک محول است
        /// </summary>
        /// <param name="productId">شماره محصول</param>
        /// <returns></returns>
        public async Task<List<FeatureValueFullDetailDTO>> GetFeaturesValuesByProductId(int productId)
        {

            return await (from product in this.DbContext.Product
                          where product.Id == productId

                          // ارتباط با ویژگی‌های مربوط به گروه این کالا
                          // با این جدول به این جهت ارتباط میدهیم تا اگر یک ویژگی به این گروه اضافه شده باشد
                          // بتواند به عنوان ویژگی‌ جدید به کاربر نمایش دهیم
                          join productGroupFeature in this.DbContext.ProductGroupFeature on product.ProductGroupId equals productGroupFeature.ProductGroupId

                          
                          //join productFeature in this.DbContext.ProductFeature on product.Id equals productFeature.ProductId

                          // برای این منظور با این جدول ارتباط میدهیم که بتوانیم اسم ویژگی و نوع آن را به دست آوریم
                          join feature in this.DbContext.Feature on productGroupFeature.FeatureId equals feature.Id

                          select new FeatureValueFullDetailDTO
                          {
                              Id = feature.Id,
                              Title = feature.Title,
                              FeatureType = feature.FeatureType,
                              IsRequired = feature.IsRequired,
                              FeatureItems = (from featureItem in this.DbContext.FeatureItem
                                              where featureItem.FeatureId == feature.Id
                                              select new FeatureItemDTO
                                              {
                                                  Id = featureItem.Id,
                                                  Value = featureItem.Value,
                                                  FeatureId = feature.Id
                                              }).ToList(),

                              //// با این جدول به این جهت که بتوان مقادیر ویژگی‌ها را به دست آورد ارتباط میدهیم
                              Value = (from productFeature in this.DbContext.ProductFeature
                                       where productFeature.FeatureId == feature.Id && productFeature.ProductId == productId
                                       select productFeature.FeatureValue).SingleOrDefault()

                          }).ToListAsync();
        }
    }
}
