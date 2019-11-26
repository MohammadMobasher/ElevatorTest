using AutoMapper;
using DataLayer.DTO;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class NewsMapper : Profile
    {

        public NewsMapper()
        {
            
            CreateMap<News, NewsDTO>();
            CreateMap<News, NewsDTO>().ReverseMap();
        }

    }
}
