using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO
{
    public class SlideShowDTO
    {
        public int Id { get; set; }
        public string ImgAddress { get; set; }
        public string URL { get; set; }
        public bool IsActive { get; set; }
        public string Title { get; set; }
        public string Alt { get; set; }

    }
}
