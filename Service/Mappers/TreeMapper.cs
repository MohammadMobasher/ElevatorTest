using AutoMapper;
using DataLayer.DTO.TreeInfo;
using DataLayer.Entities.TreeInfo;
using DataLayer.ViewModels.TreeInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mappers
{
    public class TreeMapper : Profile
    {
        public TreeMapper()
        {
            CreateMap<Tree, TreeDTO>();
            CreateMap<Tree, TreeDTO>().ReverseMap();
            CreateMap<TreeInsertViewModel, Tree>().ReverseMap();
            CreateMap<TreeUpdateViewModel, Tree>().ReverseMap();
        }
    }
}
