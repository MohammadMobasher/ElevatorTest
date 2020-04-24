using AutoMapper;
using DataLayer.DTO.ProductDiscounts;
using DataLayer.Entities;
using DataLayer.ViewModels.ProductDiscount;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class PackageDiscountMappingProfile : Profile
    {
        public PackageDiscountMappingProfile()
        {
            CreateMap<PackageDiscountDTO, ProductPackageDiscount>().ReverseMap();
            CreateMap<PackageDiscountInsertViewModel, ProductPackageDiscount>().ReverseMap();
            CreateMap<PackageDiscountUpdateViewModel, ProductPackageDiscount>().ReverseMap();
        }

    }
}
