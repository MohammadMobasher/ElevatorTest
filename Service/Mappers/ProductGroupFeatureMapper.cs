using AutoMapper;
using DataLayer.DTO;
using DataLayer.Entities;
using DataLayer.ViewModels.NewsGroup;
using DataLayer.ViewModels.ProductGroup;
using DataLayer.ViewModels.ProductGroupFeature;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class ProductGroupFeatureMapper : Profile
    {

        public ProductGroupFeatureMapper()
        {
            
            
            CreateMap<ProductGroupFeatureInsertViewModel, ProductGroupFeature>();
            
        }

    }
}
