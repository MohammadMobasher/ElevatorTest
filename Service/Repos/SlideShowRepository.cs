using DataLayer.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using DataLayer.DTO;
using AutoMapper.QueryableExtensions;

namespace Service.Repos
{
    public class SlideShowRepository : GenericRepository<SlideShow>
    {
        private readonly DatabaseContext _context;   

        public SlideShowRepository(DatabaseContext db) : base(db)
        {
            this._context = db;
        }



        /// <summary>
        /// گرفتن 5 آیتم آخر این جدول
        /// </summary>
        /// <param name="count"></param>
        public List<SlideShowDTO> GetLastItems(int count = 5)
        {
            
            return this._context.SlideShow.Where(x => x.IsActive == true)
                .ProjectTo<SlideShowDTO>()
                .ToList();



        }
    }
}
