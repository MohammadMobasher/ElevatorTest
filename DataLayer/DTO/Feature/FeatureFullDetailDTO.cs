using DataLayer.DTO.FeatureItem;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO
{
    public class FeatureFullDetailDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public FeatureTypeSSOT FeatureType { get; set; }
        public bool IsRequired { get; set; } = false;
        public bool ShowForSearch { get; set; }     

        public List<FeatureItemDTO> FeatureItems { get; set; }
    }
}
