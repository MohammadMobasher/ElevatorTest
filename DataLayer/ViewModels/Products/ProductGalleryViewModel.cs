using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Products
{
    public class ProductGalleryViewModel
    {
        public IFormFile file{ get; set; }

        public string fileAddress { get; set; }

        public List<IFormFile> galleryImage { get; set; }

        public List<string> oldGallery { get; set; }
    }
}
