using AutoMapper;
using DataLayer.DTO;
using DataLayer.Entities;
using DataLayer.ViewModels.SlideShow;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class SlideShowMapper : Profile
    {

        public SlideShowMapper()
        {
            
            CreateMap<SlideShow, SlideShowDTO>();
            CreateMap<SlideShow, SlideShowDTO>().ReverseMap();
            CreateMap<SlideShowInsertViewModel, SlideShow>().ReverseMap();
            CreateMap<SlideShowUpdateViewModel, SlideShow>().ReverseMap();
        }

    }
}
