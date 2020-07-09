using AutoMapper;
using DataLayer.ViewModels.Transportations.Tariff;
using DataLayer.ViewModels.Transportations.Car;
using DataLayer.DTO.Transportations.Cars;
using DataLayer.DTO.Transportations.Tariff;
using DataLayer.Entities.Transportation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class TransportaionMappingProfile : Profile
    {
        public TransportaionMappingProfile()
        {
            CreateMap<TariffInsertViewModel,TransportationTariff>().ReverseMap();
            CreateMap<TariffUpdateViewModel, TransportationTariff>().ReverseMap();
            CreateMap<TransportationTariffFullDto, TransportationTariff>().ReverseMap();

            CreateMap<CarsTransportaionsFullDto, TransportationTariff>().ReverseMap();
            CreateMap<CarTransportationInsertViewModel, TransportationTariff>().ReverseMap();
            CreateMap<CarTransportaionUpdateViewModel, TransportationTariff>().ReverseMap();
        }
    }
}
