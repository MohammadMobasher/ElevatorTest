using AutoMapper;
using DataLayer.Entities.Bank;
using DataLayer.ViewModels.UsersPayments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class UsersPaymentMappingProfile : Profile
    {
        public UsersPaymentMappingProfile()
        {
            CreateMap<AddUserPaymentViewModel, UsersPayment>().ReverseMap();
        }
    }
}
