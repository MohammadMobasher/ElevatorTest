using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.ViewModels.ShopProduct;

namespace Service.Mappers
{
    public class ShopProductMappingProfile : Profile
    {
        public ShopProductMappingProfile()
        {
            CreateMap<ShopProduct, ShopProductAddViewModel>().ReverseMap();
            CreateMap<ShopProduct, ShopProductAddPackageViewModel>().ReverseMap();
        }
    }
}
