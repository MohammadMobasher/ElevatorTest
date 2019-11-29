using AutoMapper;
using DataLayer.DTO;
using DataLayer.Entities;
using DataLayer.ViewModels.Feature;
using DataLayer.ViewModels.News;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class FeatureMapper : Profile
    {

        public FeatureMapper()
        {
            
            CreateMap<Feature, FeatureDTO>();
            CreateMap<Feature, FeatureDTO>().ReverseMap();
            CreateMap<FeatureInsertViewModel, Feature>().ReverseMap();
            CreateMap<FeatureUpdateViewModel, Feature>().ReverseMap();
            
        }

    }
}
