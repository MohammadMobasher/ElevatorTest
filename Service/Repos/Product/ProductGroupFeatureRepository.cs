using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Utilities;
using DataLayer.DTO;
using DataLayer.DTO.Feature;
using DataLayer.DTO.FeatureItem;
using DataLayer.DTO.ProductGroupFeature;
using DataLayer.Entities;
using DataLayer.ViewModels.ProductGroup;
using DataLayer.ViewModels.ProductGroupFeature;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos.Product
{
    public class ProductGroupFeatureRepository : GenericRepository<ProductGroupFeature>
    {
        public ProductGroupFeatureRepository(DatabaseContext dbContext) : base(dbContext)
        {

        }

        public async Task<Tuple<int, List<ProductGroupFeatureDTO>>> LoadAsyncCount(
            int productGroupId,
           int skip = -1,
           int take = -1,
           ProductGroupFeatureSearchViewModel model = null)
        {

            var query = (from productGroupFeature in this.DbContext.ProductGroupFeature
                         where productGroupFeature.ProductGroupId == productGroupId
                         join feature in this.DbContext.Feature on productGroupFeature.FeatureId equals feature.Id
                         orderby productGroupFeature.Id
                         select new ProductGroupFeatureDTO
                         {
                             Id = productGroupFeature.Id,
                             ProductGroupId = productGroupId,
                             FeatureId = feature.Id,
                             FeatureTitle = feature.Title

                         });

            if (!string.IsNullOrEmpty(model.FeatureTitle))
                query = query.Where(x => x.FeatureTitle.Contains(model.FeatureTitle));


            int Count = query.Count();

            query = query.OrderByDescending(x => x.Id);

            if (skip != -1)
                query = query.Skip((skip - 1) * take);

            if (take != -1)
                query = query.Take(take);

            return new Tuple<int, List<ProductGroupFeatureDTO>>(Count, await query.ToListAsync());
        }


        /// <summary>
        /// در این تابع لیست ویژگی‌هایی برگردانده می‌شود که متعلق به یک گروه نباشد
        /// لیستی که برمیگرداند حاوی شماره ویژگی به همراه عنوان ویژگی است
        /// </summary>
        /// <param name="productGroupId">شماره گروه مورد نظر</param>
        /// <returns></returns>
        public async Task<List<FeatureIdTitleDTO>> GetOtherFeaturesByGroupId(int productGroupId)
        {
            // لیست ویژگی‌هایی که این گروه دارد
            var hasFeature = (from productGroupFeature in this.DbContext.ProductGroupFeature
                              where productGroupFeature.ProductGroupId == productGroupId
                              join feature in this.DbContext.Feature on productGroupFeature.FeatureId equals feature.Id
                              orderby productGroupFeature.Id
                              select feature.Id);

            // لیست ویژگی‌هایی که این گروه ندارد
            var query = await (from feature_ in this.DbContext.Feature
                               where !hasFeature.Contains(feature_.Id)
                               select new FeatureIdTitleDTO
                               {
                                   Id = feature_.Id,
                                   Title = feature_.Title
                               }).ToListAsync();

            return query;
        }


        /// <summary>
        /// حذف یک آیتم از این جدول
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SweetAlertExtenstion> DeleteAsync(int id)
        {
            try
            {
                var entity = new ProductGroupFeature { Id = id };
                await DeleteAsync(entity);
                return SweetAlertExtenstion.Ok("عملیات با موفقیت انجام شد");
            }
            catch
            {
                return SweetAlertExtenstion.Error();
            }
        }



        /// <summary>
        /// در این تابع لیست ویژگی‌هایی برگردانده می‌شود که متعلق به یک گروه باشد
        /// </summary>
        /// <param name="productGroupId">شماره گروه مورد نظر</param>
        /// <returns></returns>
        public async Task<List<FeatureFullDetailDTO>> GetFeaturesByGroupId(int productGroupId)
        {
            // لیست ویژگی‌هایی که این گروه دارد
            return await (from productGroupFeature in this.DbContext.ProductGroupFeature
                          where productGroupFeature.ProductGroupId == productGroupId

                          join feature in this.DbContext.Feature on productGroupFeature.FeatureId equals feature.Id

                          orderby productGroupFeature.Id

                          select new FeatureFullDetailDTO
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
                                              }).ToList()
                          }).ToListAsync();
        }


        /// <summary>
        




        /// <summary>
        /// ثبت یک آیتم در جدول مورد نظر
        /// </summary>
        /// <param name="model">مدلی که از سمت کلاینت در حال پاس دادن آن هستیم</param>
        /// <returns></returns>
        public async Task<SweetAlertExtenstion> AddAsync(ProductGroupFeatureInsertViewModel model)
        {
            try
            {
                var entity = Mapper.Map<ProductGroupFeature>(model);
                await AddAsync(entity);
                return SweetAlertExtenstion.Ok();
            }
            catch (Exception E)
            {
                return SweetAlertExtenstion.Error();
            }

        }


        //    /// <summary>
        //    /// ثبت یک آیتم در جدول مورد نظر
        //    /// </summary>
        //    /// <param name="model">مدلی که از سمت کلاینت در حال پاس دادن آن هستیم</param>
        //    /// <returns></returns>
        //    public async Task<SweetAlertExtenstion> UpdateAsync(ProductGroupUpdateViewModel model)
        //    {

        //        try
        //        {
        //            var entity = Mapper.Map<ProductGroup>(model);
        //            await UpdateAsync(entity);
        //            return SweetAlertExtenstion.Ok();
        //        }
        //        catch
        //        {
        //            return SweetAlertExtenstion.Error();
        //        }

        //    }


        //    /// <summary>
        //    /// ثبت یک آیتم در جدول مورد نظر
        //    /// </summary>
        //    /// <param name="model">مدلی که از سمت کلاینت در حال پاس دادن آن هستیم</param>
        //    /// <returns></returns>
        //    public async Task<SweetAlertExtenstion> DeleteAsync(int Id)
        //    {

        //        try
        //        {
        //            var entity = new ProductGroup { Id = Id };
        //            await DeleteAsync(entity);
        //            return SweetAlertExtenstion.Ok("عملیات با موفقیت انجام شد");
        //        }
        //        catch
        //        {
        //            return SweetAlertExtenstion.Error();
        //        }

        //    }
        //}

        /// <summary>
        /// تمامی ویژگی های گروه یک محصول
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductGroupFeature>> GetAllProductGroupFeature(int groupId)
            => await TableNoTracking.Include(a => a.Feature).Where(a => a.ProductGroupId == groupId).ToListAsync();


    }
}
