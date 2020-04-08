using AutoMapper;
using DataLayer.DTO;
using DataLayer.DTO.Feature;
using DataLayer.Entities;
using DataLayer.ViewModels.Feature;
using DataLayer.ViewModels.News;
using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.DataLayer.DTO.Products;
namespace Service.Mappers
{
    public class FeatureMapper : Profile
    {

        public FeatureMapper()
        {
            
            CreateMap<Feature, FeatureFullDetailDTO>();
            CreateMap<Feature, FeatureIdTitleDTO>();
            CreateMap<Feature, FeatureFullDetailDTO>().ReverseMap();
            CreateMap<Feature, ProductPackageQuestionDTO>().ReverseMap();
            CreateMap<FeatureInsertViewModel, Feature>().ReverseMap();
            CreateMap<FeatureUpdateViewModel, Feature>().ReverseMap();
            CreateMap<FeatureQuestionsViewModel, Feature>().ReverseMap();
            
        }

    }
}
