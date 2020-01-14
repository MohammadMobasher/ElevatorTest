using AutoMapper;
using DataLayer.DTO.Condition;
using DataLayer.Entities;
using DataLayer.ViewModels.Condition;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class ConditionMapper : Profile
    {

        public ConditionMapper()
        {
            CreateMap<ConditionInsertViewModel, Condition>();
            CreateMap<Condition, ConditionDTO>();
            CreateMap<ConditionUpdateViewModel, Condition>();
        }
    }
}
