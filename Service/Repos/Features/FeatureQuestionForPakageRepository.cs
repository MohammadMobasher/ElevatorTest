using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Utilities;
using DataLayer.DTO.FeatureQuestionForPakage;
using DataLayer.Entities.Features;
using DataLayer.ViewModels.FeatureQuestionForPakage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos.Features
{
    public class FeatureQuestionForPakageRepository : GenericRepository<FeatureQuestionForPakage>
    {
        public FeatureQuestionForPakageRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }


        public async Task<Tuple<int, List<FeatureQuestionForPakageDTO>>> LoadAsyncCount(
            int skip = -1,
            int take = -1,
            FeatureQuestionForPakageSearchViewModel model = null)
        {
            var query = Entities.ProjectTo<FeatureQuestionForPakageDTO>();

            if (!string.IsNullOrEmpty(model.QuestionTitle))
                query = query.Where(x => x.QuestionTitle.Contains(model.QuestionTitle));


            if (model.FeatureId != -1)
                query = query.Where(x => x.FeatureId == model.FeatureId);

            if (model.GroupId != -1)
                query = query.Where(x => x.GroupId == model.GroupId);


            int Count = query.Count();

            query = query.OrderByDescending(x => x.Id);


            if (skip != -1)
                query = query.Skip((skip - 1) * take);

            if (take != -1)
                query = query.Take(take);



            return new Tuple<int, List<FeatureQuestionForPakageDTO>>(Count, await query.ToListAsync());
        }


        /// <summary>
        /// ثبت یک رکورد در این جدول
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<SweetAlertExtenstion> Insert(FeatureQuestionForPakageInsertViewModel model)
        {
            try
            {
                var entity = Mapper.Map<FeatureQuestionForPakage>(model);
                await AddAsync(entity);
                return SweetAlertExtenstion.Ok();
            }
            catch(Exception e)
            {
                return SweetAlertExtenstion.Error();
            }
        }


        public async Task<SweetAlertExtenstion> UpdateAsync(FeatureQuestionForPakageUpdateViewModel model)
        {
            try
            {
                var entity = await GetByIdAsync(model.Id);

                entity = (FeatureQuestionForPakage)Mapper.Map(model, entity, typeof(FeatureQuestionForPakageInsertViewModel), typeof(FeatureQuestionForPakage));

                

                await DbContext.SaveChangesAsync();
                return SweetAlertExtenstion.Ok();
            }
            catch (Exception e)
            {
                return SweetAlertExtenstion.Error();
            }
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
                return SweetAlertExtenstion.Ok();
            }
            catch
            {
                return SweetAlertExtenstion.Error();
            }

        }
    }

}
