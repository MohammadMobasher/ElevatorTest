using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Feature
{
    public class FeatureSearchViewModel
    {

        public string Title { get; set; }

        public FeatureTypeSSOT? FeatureType { get; set; } = null;

        public bool? IsRequired { get; set; } = null;
    }
}
