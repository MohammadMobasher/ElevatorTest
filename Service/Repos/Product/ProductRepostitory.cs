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
    public class ProductRepostitory : GenericRepository<DataLayer.Entities.Product>
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ProductDiscountRepository _productDiscountRepository;

        public ProductRepostitory(DatabaseContext dbContext,
            IHostingEnvironment hostingEnvironment,
            ProductDiscountRepository productDiscountRepository) : base(dbContext)
        {
            _hostingEnvironment = hostingEnvironment;
            _productDiscountRepository = productDiscountRepository;
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


        public async Task ChangeStateProduct(int id)
        {
            var model = await GetByIdAsync(id);

            model.IsActive = model.IsActive ==null ?true : !model.IsActive;
            await UpdateAsync(model);
        }


        public async Task ChangeSpecial(int id)
        {
            var model = await GetByIdAsync(id);

            model.IsSpecialSell = !model.IsSpecialSell;

            await UpdateAsync(model);
        }

        /// <summary>
        /// شماره محصولاتی که در یک گروه قرار دارد
        /// </summary>
        /// <param name="productGroupId">شماره گروه مورد نظر</param>
        /// <returns></returns>
        public async Task<List<int>> GetProductIdsByGroupId(int productGroupId)
        {
            return await Entities.Where(x => x.ProductGroupId == productGroupId).Select(x=> x.Id).ToListAsync();
        }



        /// <summary>
        /// تعداد کالاهایی که دارای یک واحد میباشند
        /// </summary>
        /// <param name="productUnitId">شماره واحد مورد نظر</param>
        /// <returns></returns>
        public async Task<int> NumProductByProductUnitId(int productUnitId)
        {
            var result = await Entities.Where(x => x.ProductUnitId == productUnitId).ToListAsync();
            if (result != null && result.Count > 0)
                return result.Count;
            return 0;

        }

        /// <summary>
        /// تعداد کالاهایی که دارای یک واحد میباشند
        /// </summary>
        /// <param name="productUnitId">شماره واحد مورد نظر</param>
        /// <returns></returns>
        public async Task<bool> DeleteByProductUnitId(int productUnitId)
        {
            try
            {
                var result = await Entities.Where(x => x.ProductUnitId == productUnitId).ToListAsync();

                if (await _productDiscountRepository.DeleteByProductIds(result.Select(x => x.Id).ToList()))
                    await DeleteRangeAsync(result);
                else
                    return false;
                return true;
            }
            catch(Exception e)
            {
                return false;
            }

        }

        /// <summary>
        /// اضافه کردن تعداد بازدید کننده
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task VisitPlus(int id)
        {
            var model = await GetByIdAsync(id);

            model.Visit = model.Visit + 1;
            await UpdateAsync(model);
        }

        /// <summary>
        /// اضافه کردن تعداد لایک
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> AddLike(int id)
        {
            var model = await GetByIdAsync(id);

            model.Like = model.Like + 1;
            await UpdateAsync(model);

            return model.Like;
        }


        /// <summary>
        /// اضافه کردن تعداد دیس لایک
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> AddDisLike(int id)
        {
            var model = await GetByIdAsync(id);

            model.DisLike = model.DisLike + 1;
            await UpdateAsync(model);

            return model.DisLike;
        }
    }
}
