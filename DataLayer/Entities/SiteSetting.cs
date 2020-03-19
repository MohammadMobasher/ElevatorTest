using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class SiteSetting : BaseEntity<int>
    {

        public string Logo { get; set; }

        public string TabIcon { get; set; }


        public string TwitterURL { get; set; }
        public string TelegramURL { get; set; }
        public string WhatsAppURL { get; set; }
        public string InstaURL { get; set; }

    }
}
