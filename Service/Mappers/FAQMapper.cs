using AutoMapper;
using DataLayer.DTO.FAQs;
using DataLayer.Entities.FAQs;
using DataLayer.ViewModels.FAQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class FAQMapper : Profile
    {

        public FAQMapper()
        {
            CreateMap<FAQ, FAQDTO>();
            CreateMap<FAQ, FAQDTO>().ReverseMap();
            CreateMap<FAQInsertViewModel, FAQ>().ReverseMap();
            CreateMap<FAQUpdateViewModel, FAQ>().ReverseMap();
        }

    }
}
