﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Utilities;
using DataLayer.DTO.Feature;
using DataLayer.DTO.FeatureItem;
using DataLayer.DTO.Products;
using DataLayer.ViewModels.ProductPackage;
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
    public class ProductPackageRepostitory : GenericRepository<DataLayer.Entities.ProductPackage>
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ProductDiscountRepository _productDiscountRepository;
        public ProductPackageRepostitory(DatabaseContext dbContext,
            IHostingEnvironment hostingEnvironment,
            ProductDiscountRepository productDiscountRepository) : base(dbContext)
        {
            _hostingEnvironment = hostingEnvironment;
            _productDiscountRepository = productDiscountRepository;
        }


        public async Task<Tuple<int, List<ProductPackageFullDTO>>> LoadAsyncCount(
         int skip = -1,
         int take = -1,
         ProductPackageSearchViewModel model = null)
        {
            var query = Entities.ProjectTo<ProductPackageFullDTO>();


            if (model.Id != null)
                query = query.Where(x => x.Id == model.Id);

            if (!string.IsNullOrEmpty(model.Title))
                query = query.Where(x => x.Title.Contains(model.Title));

            if (model.Discount != null)
            {
                query = query.Where(x => x.Discount == model.Discount);
            }

            if (model.Title != null)
            {
                query = query.Where(x => x.Title.Contains(model.Title));
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

            return new Tuple<int, List<ProductPackageFullDTO>>(Count, await query.ToListAsync());
        }


        public async Task<int> CreateAsync(ProductPackageInsertViewModel vm, IFormFile file)
        {

            vm.IndexPic = await MFile.Save(file, FilePath.productPackage.GetDescription());

            var mapModel = Map(vm);

            mapModel.IsManager = true;

            await AddAsync(mapModel);

            return mapModel.Id;
        }

        public async Task<int> UpdateProduct(ProductPackageUpdateViewModel vm, IFormFile file)
        {
            if (file != null)
            {
                if (vm.IndexPic != null)
                {
                    try
                    {

                        var WebContent = _hostingEnvironment.WebRootPath;

                        System.IO.File.Delete(WebContent + FilePath.productPackage.GetDescription());
                    }
                    catch (Exception)
                    {
                        return 0;
                    }
                }
                vm.IndexPic = await MFile.Save(file, FilePath.productPackage.GetDescription());
                
            }
            var model = GetById(vm.Id);

            Mapper.Map(vm, model);

            await DbContext.SaveChangesAsync();

            return model.Id;
        }

        public async Task ChangeStateProduct(int id)
        {
            var model = await GetByIdAsync(id);

            model.IsActive = !model.IsActive;
            await UpdateAsync(model);
        }
        public async Task ChangeSpecial(int id)
        {
            var model = await GetByIdAsync(id);

            model.IsSpecialPackage = !model.IsSpecialPackage;

            await UpdateAsync(model);
        }


    }
}