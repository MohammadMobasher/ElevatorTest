using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.SiteSetting
{
    public class SiteSettingInsertViewModel
    {
        public string Logo { get; set; }
        public IFormFile LogoFile { get; set; }

        public string TabIcon { get; set; }
        public IFormFile TabIconFile { get; set; }




        public string TwitterURL { get; set; }
        public string TelegramURL { get; set; }
        public string WhatsAppURL { get; set; }
        public string InstaURL { get; set; }


    }
}
