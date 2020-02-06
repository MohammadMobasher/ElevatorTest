using AutoMapper;
using DataLayer.DTO.Products;
using DataLayer.Entities;
using DataLayer.ViewModels.ProductPackage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class ProductPackageMappingProfile:Profile
    {

        public ProductPackageMappingProfile()
        {
            CreateMap<ProductPackage, ProductPackageInsertViewModel>().ReverseMap();
            CreateMap<ProductPackage, ProductPackageUpdateViewModel>().ReverseMap();
            CreateMap<ProductPackageDetails, ProductPackageDetailFullDTO>().ReverseMap();
            CreateMap<ProductPackage, ProductPackageFullDTO>().ReverseMap();
        }

    }
}
