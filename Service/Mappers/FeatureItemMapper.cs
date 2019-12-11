using AutoMapper;
using DataLayer.DTO;
using DataLayer.DTO.FeatureItem;
using DataLayer.Entities;
using DataLayer.ViewModels.Feature;
using DataLayer.ViewModels.News;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class FeatureItemMapper : Profile
    {

        public FeatureItemMapper()
        {
            
            CreateMap<FeatureItem, FeatureItemDTO>();

            
        }

    }
}
