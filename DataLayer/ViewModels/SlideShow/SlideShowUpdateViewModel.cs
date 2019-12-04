using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.SlideShow
{
    public class SlideShowUpdateViewModel
    {
        public int Id { get; set; }
        public string ImgAddress { get; set; }
        public string URL { get; set; }
        
        public string Title { get; set; }
        public string Alt { get; set; }

        /// <summary>
        /// فایلی که ارسال شده برای ذخیره
        /// </summary>
        public IFormFile ImageFile { get; set; }
    }
}
