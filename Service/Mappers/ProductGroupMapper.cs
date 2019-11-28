using AutoMapper;
using DataLayer.DTO;
using DataLayer.Entities;
using DataLayer.ViewModels.NewsGroup;
using DataLayer.ViewModels.ProductGroup;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class ProductGroupMapper : Profile
    {

        public ProductGroupMapper()
        {
            
            CreateMap<ProductGroup, ProductGroupDTO>();
            CreateMap<ProductGroupInsertViewModel, ProductGroup>();
            CreateMap<ProductGroupUpdateViewModel, ProductGroup>();
        }

    }
}
