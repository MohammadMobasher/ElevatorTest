using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.ProductPackage
{
    public class ProductPackageInsertViewModel
    {
        public string Title { get; set; }

        public string ShortDiscription { get; set; }

        public string Text { get; set; }

        public string IndexPic { get; set; }


        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
