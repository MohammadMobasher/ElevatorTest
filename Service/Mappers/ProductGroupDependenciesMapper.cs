using AutoMapper;
using DataLayer.DTO.ProductGroupDependencies;
using DataLayer.Entities;
using DataLayer.ViewModels.ProductGroupDependencies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class ProductGroupDependenciesMapper : Profile
    {
        public ProductGroupDependenciesMapper()
        {
            CreateMap<ProductGroupDependencies, ProductGroupDependenciesFullDTO>();
            CreateMap<ProductGroupDependenciesInsertViewModel, ProductGroupDependencies>();
        }
    }
}
