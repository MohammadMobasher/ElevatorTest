using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.LogoManufactory
{
    public class UpdateLogoManufactoryViewModel
    {
        public int Id { get; set; }
        

        public string Title { get; set; }


        
        public string URL { get; set; }


        public string Alt { get; set; }

        public string AddressImg { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
