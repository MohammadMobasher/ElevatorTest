using AutoMapper;
using DataLayer.DTO.ProductDiscounts;
using DataLayer.Entities;
using DataLayer.ViewModels.ProductDiscount;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class ProductDiscountMappingProfile :Profile
    {
        public ProductDiscountMappingProfile()
        {
            CreateMap<ProductDiscountDTO, ProductDiscount>().ReverseMap();
            CreateMap<ProductDiscountInsertViewModel, ProductDiscount>().ReverseMap();
            CreateMap<ProductDiscountUpdateViewModel, ProductDiscount>().ReverseMap();
        }

    }
}
