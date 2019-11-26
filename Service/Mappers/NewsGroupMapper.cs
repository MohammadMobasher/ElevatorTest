using AutoMapper;
using DataLayer.DTO;
using DataLayer.Entities;
using DataLayer.ViewModels.NewsGroup;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class NewsGroupMapper : Profile
    {

        public NewsGroupMapper()
        {
            
            CreateMap<NewsGroup, NewsGroupDTO>();
            CreateMap<NewsGroup, NewsGroupDTO>().ReverseMap();

            CreateMap<NewsGroupInsertViewModel, NewsGroup>();
            CreateMap<NewsGroupUpdateViewModel, NewsGroup>();
        }

    }
}
