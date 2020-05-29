using AutoMapper;
using DataLayer.DTO.ShopOrderStatus;
using DataLayer.Entities;
using DataLayer.ViewModels.ShopOrderStatus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class ShopOrderStatusMapper : Profile
    {


        public ShopOrderStatusMapper()
        {

            CreateMap<ShopOrderStatus, ShopOrderStatusDTO>();
            CreateMap<ShopOrderStatus, ShopOrderStatusDTO>().ReverseMap();
            CreateMap<ShopOrderStatusInsertViewModel, ShopOrderStatus>().ReverseMap();

        }

    }
}
