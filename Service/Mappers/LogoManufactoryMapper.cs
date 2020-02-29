using AutoMapper;
using DataLayer.DTO.FAQs;
using DataLayer.DTO.LogoManufactory;
using DataLayer.Entities;
using DataLayer.Entities.FAQs;
using DataLayer.ViewModels.FAQ;
using DataLayer.ViewModels.LogoManufactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class LogoManufactoryMapper : Profile
    {

        public LogoManufactoryMapper()
        {
            CreateMap<LogoManufactory, LogoManufactoryDTO>();
            CreateMap<LogoManufactory, LogoManufactoryDTO>().ReverseMap();
            CreateMap<UpdateLogoManufactoryViewModel, LogoManufactory>().ReverseMap();
            CreateMap<InsertLogoManufactoryListView, LogoManufactory>().ReverseMap();
        }

    }
}
