using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Log:BaseEntity<int>
    {
        public string Text { get; set; }
    }
}
