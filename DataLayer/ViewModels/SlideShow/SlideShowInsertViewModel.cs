﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.SlideShow
{
    public class SlideShowInsertViewModel
    {
        public string ImgAddress { get; set; }
        public string URL { get; set; }
        public int Alt { get; set; }

        /// <summary>
        /// فایلی که ارسال شده برای ذخیره
        /// </summary>
        public IFormFile ImageFile { get; set; }
    }
}
