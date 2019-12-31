using DataLayer.DTO.FeatureItem;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO.Feature
{
    public class FeatureValueFullDetailDTO
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public FeatureTypeSSOT FeatureType { get; set; }
        public bool IsRequired { get; set; } = false;
        public List<FeatureItemDTO> FeatureItems { get; set; }

    }
}
