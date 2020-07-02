using DataLayer.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO.Condition
{
    public class ConditionDTO : BaseMapping<ConditionDTO, Entities.Condition, int>
    {
        public string Title { get; set; }

        public string Name { get; set; }

    }
}
