using AutoMapper;
using DataLayer.DTO.Products;
using DataLayer.Entities;
using DataLayer.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductFullDTO, Product>().ReverseMap();
            CreateMap<ProductInsertViewModel, Product>().ReverseMap();

            CreateMap<ProductPriceDTO, Product>().ReverseMap();
            CreateMap<ProductFastPriceEditViewModel, Product>().ReverseMap();
        }
    }
}
