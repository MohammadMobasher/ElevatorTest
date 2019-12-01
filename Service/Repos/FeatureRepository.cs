using DataLayer.DTO;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities;
using AutoMapper;
using DataLayer.ViewModels.News;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using DataLayer.ViewModels.Feature;

namespace Service.Repos
{
    public class FeatureRepository : GenericRepository<Feature>
    {
        
        public FeatureRepository(DatabaseContext dbContext) : base(dbContext)
        {
            
        }

        


        //TODO
        /// <summary>
        /// گرفتن اطلاعات کامل یک خبر
        /// </summary>
        /// <param name="id">شماره خبر</param>
        /// <returns></returns>
        public FeatureDTO GetItemDetail(int id)
        {
            return this.Entities.ProjectTo<FeatureDTO>().Where(x => x.Id == id).SingleOrDefault();
        }


        //TODO
        /// <summary>
        /// گرفتن اطلاعات کامل یک خبر
        /// </summary>
        /// <param name="id">شماره خبر</param>
        /// <returns></returns>
        public async Task<FeatureDTO> GetItemDetailAsync(int id)
        {
            var result = this.Entities.ProjectTo<FeatureDTO>().Where(x => x.Id == id).SingleOrDefault();
            return result;
        }



        /// <summary>
        /// ثبت اطلاعات در جدول مورد نظر 
        /// </summary>
        /// <param name="model">مدلی که باید به این تابع پاس داده شود  تا بتوان آن را ذخیره کرد</param>
        /// <returns></returns>
        public async Task<SweetAlertExtenstion> Insert(FeatureInsertViewModel model)
        {
            try
            {
                var entity = Mapper.Map<Feature>(model);

                
                await AddAsync(entity);
                return SweetAlertExtenstion.Ok();
            }
            catch
            {
                return SweetAlertExtenstion.Error();
            }
        }


        public async Task<SweetAlertExtenstion> UpdateAsync(FeatureUpdateViewModel model)
        {
            try
            {
                var entity = await GetByIdAsync(model.Id);

                entity = (Feature)Mapper.Map(model, entity, typeof(FeatureUpdateViewModel), typeof(Feature));

                

                await DbContext.SaveChangesAsync();
                return SweetAlertExtenstion.Ok();
            }
            catch(Exception e)
            {
                return SweetAlertExtenstion.Error();
            }
        }


        public async Task<Tuple<int, List<FeatureDTO>>> LoadAsyncCount(
            int skip = -1,
            int take = -1,
            FeatureSearchViewModel model = null)
        {
            var query = Entities.ProjectTo<FeatureDTO>();

            if (!string.IsNullOrEmpty(model.Title))
                query = query.Where(x => x.Title.Contains(model.Title));


            if (model.FeatureType != null)
                query = query.Where(x => x.FeatureType == model.FeatureType);


            int Count = query.Count();

            query = query.OrderByDescending(x => x.Id);


            if (skip != -1)
                query = query.Skip((skip - 1) * take);

            if (take != -1)
                query = query.Take(take);



            return new Tuple<int, List<FeatureDTO>>(Count, await query.ToListAsync());
        }


        /// <summary>
        /// ثبت یک آیتم در جدول مورد نظر
        /// </summary>
        /// <param name="Id">شماره خبر</param>
        /// <returns></returns>
        public async Task<SweetAlertExtenstion> DeleteAsync(int Id)
        {

            try
            {
                var entity = await GetByIdAsync(Id);

                await DeleteAsync(entity);
                return SweetAlertExtenstion.Ok("عملیات با موفقیت انجام شد");
            }
            catch
            {
                return SweetAlertExtenstion.Error();
            }

        }

    }
}
