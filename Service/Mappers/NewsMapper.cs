using AutoMapper;
using DataLayer.DTO;
using DataLayer.Entities;
using DataLayer.ViewModels.News;
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
            CreateMap<InsertNewsListView, News>().ReverseMap();
            CreateMap<NewsUpdateViewModel, News>().ReverseMap();
            
        }

    }
}
