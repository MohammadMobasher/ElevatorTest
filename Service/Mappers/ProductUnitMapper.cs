using AutoMapper;
using DataLayer.DTO;
using DataLayer.DTO.ProductUnit;
using DataLayer.Entities;
using DataLayer.ViewModels.NewsGroup;
using DataLayer.ViewModels.ProductGroup;
using DataLayer.ViewModels.ProductUnit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class ProductUnitMapper : Profile
    {

        public ProductUnitMapper()
        {
            
            CreateMap<ProductUnit, ProductUnitDTO>();
            CreateMap<ProductUnitInsertViewModel, ProductUnit>();
            CreateMap<ProductUnitUpdateViewModel, ProductUnit>();
        }

    }
}
