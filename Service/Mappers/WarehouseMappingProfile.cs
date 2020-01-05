using AutoMapper;
using DataLayer.DTO.WarehouseDTO;
using DataLayer.Entities.Warehouse;
using DataLayer.ViewModels.Warehouse;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class WarehouseMappingProfile : Profile
    {
        public WarehouseMappingProfile()
        {
            CreateMap<WarehouseFullDTO, Warehouse>().ReverseMap();
            CreateMap<WarehouseCreateViewModel, Warehouse>().ReverseMap();
            CreateMap<WarehouseUpdateViewModel, Warehouse>().ReverseMap();
        }
    }
}
