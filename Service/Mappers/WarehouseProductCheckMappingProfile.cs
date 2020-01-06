using AutoMapper;
using DataLayer.DTO.WarehouseProductCheckDTO;
using DataLayer.Entities.Warehouse;
using DataLayer.ViewModels.WarehouseProductChecks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class WarehouseProductCheckMappingProfile : Profile
    {
        public WarehouseProductCheckMappingProfile()
        {
            CreateMap<WarehouseProductCheckInsertViewModel, WarehouseProductCheck>().ReverseMap();
            CreateMap<WarehouseProductCheckUpdateViewModel, WarehouseProductCheck>().ReverseMap();
            CreateMap<WarehouseProductCheckFullDTO, WarehouseProductCheck>().ReverseMap();
        }
    }
}
