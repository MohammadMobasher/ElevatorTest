using DataLayer.BaseClasses;
using DataLayer.Entities.FAQs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO.FAQs
{
    public class FaqGroupDTO : BaseMapping<FaqGroupDTO,FaqGroup,int>
    {
        public string Title { get; set; }

        public string Icon { get; set; }
    }
}
