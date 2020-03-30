﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Utilities;
using DataLayer.DTO;
using DataLayer.Entities;
using DataLayer.ViewModels.ProductGroup;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos.Product
{
    public class ProductGroupRepository : GenericRepository<ProductGroup>
    {
        public ProductGroupRepository(DatabaseContext dbContext) : base(dbContext)
        {

        }

        public async Task<Tuple<int, List<ProductGroupDTO>>> LoadAsyncCount(
           int skip = -1,
           int take = -1,
           ProductGroupSearchViewModel model = null)
        {
            var query = Entities.ProjectTo<ProductGroupDTO>();


            if (model.Id != null)
                query = query.Where(x => x.Id == model.Id);

            if (!string.IsNullOrEmpty(model.Title))
                query = query.Where(x => x.Title.Contains(model.Title));

            if (model.ParentId != null)
                query = query.Where(x => x.ParentId == model.ParentId);

            int Count = query.Count();

            query = query.OrderByDescending(x => x.Id);


            if (skip != -1)
                query = query.Skip((skip - 1) * take);

            if (take != -1)
                query = query.Take(take);

            return new Tuple<int, List<ProductGroupDTO>>(Count, await query.ToListAsync());
        }


        /// <summary>
        /// گرفتن تمام پدرها
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductGroupDTO>> GetParentsAsync()
        {
            return await Entities.ProjectTo<ProductGroupDTO>().Where(x => x.Parent == null).ToListAsync();
        }

        /// <summary>
        /// گرفتن تمام پدرها
        /// </summary>
        /// <returns></returns>
        public List<ProductGroupDTO> GetParents()
        {
            return Entities.ProjectTo<ProductGroupDTO>().Where(x => x.Parent == null).ToList();
        }



        /// <summary>
        /// گرفتن تمام فرزندان یک پدر تا یک سطح
        /// </summary>
        /// <param name="parentId">شماره پدر</param>
        /// <param name="includeItSelf">خروجی مورد نظر شامل خود پدر هم بشود یا نه</param>
        /// <returns></returns>
        public async Task<List<ProductGroupDTO>> GetByParentId(int parentId, bool includeItSelf = false)
        {
            if(!includeItSelf)
                return await TableNoTracking.Where(x => x.ParentId == parentId).ProjectTo<ProductGroupDTO>().ToListAsync();
            //شامل خودش
            return await TableNoTracking.Where(x => x.ParentId == parentId || x.Id == parentId).ProjectTo<ProductGroupDTO>().ToListAsync();
        }


        /// <summary>
        /// ثبت یک آیتم در جدول مورد نظر
        /// </summary>
        /// <param name="model">مدلی که از سمت کلاینت در حال پاس دادن آن هستیم</param>
        /// <returns></returns>
        public async Task<SweetAlertExtenstion> AddAsync(ProductGroupInsertViewModel model)
        {

            try
            {
                var entity = Mapper.Map<ProductGroup>(model);
                await AddAsync(entity);
                return SweetAlertExtenstion.Ok();
            }
            catch
            {
                return SweetAlertExtenstion.Error();
            }

        }


        /// <summary>
        /// ثبت یک آیتم در جدول مورد نظر
        /// </summary>
        /// <param name="model">مدلی که از سمت کلاینت در حال پاس دادن آن هستیم</param>
        /// <returns></returns>
        public async Task<SweetAlertExtenstion> UpdateAsync(ProductGroupUpdateViewModel model)
        {

            try
            {
                var entity = Mapper.Map<ProductGroup>(model);
                await UpdateAsync(entity);
                return SweetAlertExtenstion.Ok();
            }
            catch
            {
                return SweetAlertExtenstion.Error();
            }

        }


        /// <summary>
        /// ثبت یک آیتم در جدول مورد نظر
        /// </summary>
        /// <param name="model">مدلی که از سمت کلاینت در حال پاس دادن آن هستیم</param>
        /// <returns></returns>
        public async Task<SweetAlertExtenstion> DeleteAsync(int Id)
        {

            try
            {
                var entity = new ProductGroup { Id = Id };
                await DeleteAsync(entity);
                return SweetAlertExtenstion.Ok("عملیات با موفقیت انجام شد");
            }
            catch
            {
                return SweetAlertExtenstion.Error();
            }

        }

        /// <summary>
        /// گرفتن تمام اطلاعات
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductGroupDTO>> GetAllAsync()
        {
            return await Entities.AsNoTracking().ProjectTo<ProductGroupDTO>().ToListAsync();
        }

        /// <summary>
        /// گرفتن تمام اطلاعات
        /// </summary>
        /// <returns></returns>
        public List<ProductGroupDTO> GetAll()
        {
            return Entities.AsNoTracking().ProjectTo<ProductGroupDTO>().ToList();
        }

    }
}
