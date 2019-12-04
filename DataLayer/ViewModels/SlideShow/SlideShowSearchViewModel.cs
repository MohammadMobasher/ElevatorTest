using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.SlideShow
{
    public class SlideShowSearchViewModel
    {
        public string ImgAddress { get; set; }
        public string URL { get; set; }
        public bool IsActive { get; set; }
        public string Title { get; set; }
        public string Alt { get; set; }
    }
}
