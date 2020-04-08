using AutoMapper;
using DataLayer.DTO.FeatureQuestionForPakage;
using DataLayer.Entities.Features;
using DataLayer.ViewModels.FeatureQuestionForPakage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class FeatureQuestionForPakageMapper : Profile
    {
        public FeatureQuestionForPakageMapper()
        {
            CreateMap<FeatureQuestionForPakage, FeatureQuestionForPakageDTO>();
            CreateMap<FeatureQuestionForPakageInsertViewModel, FeatureQuestionForPakage>();
            CreateMap<FeatureQuestionForPakageUpdateViewModel, FeatureQuestionForPakage>();
        }
    }
}
