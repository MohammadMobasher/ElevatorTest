using AutoMapper;
using DataLayer.DTO;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class NewsTagMapper : Profile
    {

        public NewsTagMapper()
        {
            
            CreateMap<NewsTag, NewsTagDTO>();
            CreateMap<NewsTag, NewsTagDTO>().ReverseMap();
        }

    }
}
