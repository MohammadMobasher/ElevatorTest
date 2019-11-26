using DataLayer.DTO;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Repos
{
    public class NewsRepository : GenericRepository<News>
    {
        
        public NewsRepository(DatabaseContext dbContext) : base(dbContext)
        {
            
        }

        /// <summary>
        /// گرفتن 10 خبر محبوب آخر
        /// این تابع بر این اساس است که اخبار موجود را به نزولی مرتب میکند
        /// و 10 خبر آخر را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public List<NewsDTO> GetFavoritNews()
        {
            return this.Entities
                .ProjectTo<NewsDTO>()
                .OrderByDescending(x => x.ViewCount)
                .Skip(0)
                .Take(10)
                .ToList();
        }


        //TODO
        /// <summary>
        /// گرفتن اطلاعات کامل یک خبر
        /// </summary>
        /// <param name="id">شماره خبر</param>
        /// <returns></returns>
        public NewsDTO GetItemDetail(int id)
        {
            return this.Entities.ProjectTo<NewsDTO>().Where(x => x.Id == id).SingleOrDefault();
        }


        //TODO
        /// <summary>
        /// گرفتن اطلاعات کامل یک خبر
        /// </summary>
        /// <param name="id">شماره خبر</param>
        /// <returns></returns>
        public async Task<NewsDTO> GetItemDetailAsync(int id)
        {
            return this.Entities.ProjectTo<NewsDTO>().Where(x => x.Id == id).SingleOrDefault();
        }



    }
}
