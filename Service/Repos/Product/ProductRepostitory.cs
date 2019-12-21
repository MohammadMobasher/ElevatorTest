using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Utilities;
using DataLayer.DTO.Products;
using DataLayer.ViewModels.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
    }
}
