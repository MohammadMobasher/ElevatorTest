using AutoMapper;
using DataLayer.DTO;
using DataLayer.DTO.SuggestionsAndComplaint;
using DataLayer.Entities;
using DataLayer.ViewModels.NewsGroup;
using DataLayer.ViewModels.SuggestionsAndComplaint;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class SuggestionsAndComplaintMapper : Profile
    {

        public SuggestionsAndComplaintMapper()
        {
            
            CreateMap<DataLayer.Entities.SuggestionsAndComplaint, SuggestionsAndComplaintDTO>();
            CreateMap<DataLayer.Entities.SuggestionsAndComplaint, SuggestionsAndComplaintDTO>().ReverseMap();
            CreateMap<SuggestionsAndComplaintInsertViewModel, DataLayer.Entities.SuggestionsAndComplaint>().ReverseMap();
        }

    }
}
