using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.News
{
    public class NewsUpdateViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SummeryNews { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string ImageAddress { get; set; }
        public int NewsGroupId { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
