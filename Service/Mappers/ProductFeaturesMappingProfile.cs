using AutoMapper;
using DataLayer.DTO.ProductFeatures;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class ProductFeaturesMappingProfile:Profile
    {
        public ProductFeaturesMappingProfile()
        {
            CreateMap<ProductFeaturesFullDTO, ProductFeature>().ReverseMap();
        }
    }
}
