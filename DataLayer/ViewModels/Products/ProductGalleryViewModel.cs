﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Products
{
    public class ProductGalleryViewModel
    {
        public IFormFile file{ get; set; }

        public List<IFormFile> gallery { get; set; }
    }
}
