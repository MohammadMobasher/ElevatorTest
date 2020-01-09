using AutoMapper;
using DataLayer.DTO.FAQs;
using DataLayer.Entities.FAQs;
using DataLayer.ViewModels.FAQ;
using DataLayer.ViewModels.FaqGroup;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class FaqGroupMapper : Profile
    {

        public FaqGroupMapper()
        {
            CreateMap<FaqGroup, FaqGroupDTO>();
            CreateMap<FaqGroup, FaqGroupDTO>().ReverseMap();
            CreateMap<FaqGroupInsertViewModel, FaqGroup>().ReverseMap();
            CreateMap<FaqGroupUpdateViewModel, FaqGroup>().ReverseMap();
        }

    }
}
