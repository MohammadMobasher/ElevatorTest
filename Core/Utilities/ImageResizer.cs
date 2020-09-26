using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Core.Utilities
{
    public class ImageResizer
    {

        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public bool TrimImage { get; set; }
        public ImageFormat SaveFormat { get; set; }

        public float ScaleSize { get; set; }

        public ImageResizer()
        {
            MaxX = MaxY = 150;
            TrimImage = false;
            SaveFormat = ImageFormat.Jpeg;
        }

        public ImageResizer(ImageSize size)
        {
            TrimImage = false;
            SaveFormat = ImageFormat.Jpeg;
        }


        bool ResizeImage(string source, string target, ImageSize imageSize, string outputName)
        {
            var Scale = (float)imageSize / 10;
            using (Image src = Image.FromFile(source, true))
            {
                float width = (src.Width * Scale);
                float height = (src.Height * Scale);

                float scale = Math.Min(width / src.Width, height / src.Height);

                Image bmp = new Bitmap((int)width, (int)height);

                var graph = Graphics.FromImage(bmp);

                //graph.InterpolationMode = InterpolationMode.High;
                //graph.CompositingQuality = CompositingQuality.HighQuality;
                //graph.SmoothingMode = SmoothingMode.AntiAlias;

                var scaleWidth = (int)(src.Width * scale);
                var scaleHeight = (int)(src.Height * scale);

                graph.DrawImage(src, ((int)width - scaleWidth) / 2, ((int)height - scaleHeight) / 2, scaleWidth, scaleHeight);
                bmp.Save(target+ outputName);
            }

            return true;
        }


        public bool Resize(string source, string target)
        {
            ResizeImage(source, target, ImageSize.Small,"Small.jpg");
            ResizeImage(source, target, ImageSize.Medium, "Medium.jpg");
            ResizeImage(source, target, ImageSize.Large, "Large.jpg");
            ResizeImage(source, target, ImageSize.VerySmall, "VerySmall.jpg");

            return true;
            //using (Image src = Image.FromFile(source, true))
            //{
            //    if (src != null)
            //    {
            //        if (src.Width >= 1281)
            //        {

            //        }

            //        float width = (src.Width * ScaleSize);
            //        float height = (src.Height * ScaleSize);

            //        float scale = Math.Min(width / src.Width, height / src.Height);

            //        Image bmp = new Bitmap((int)width, (int)height);

            //        var graph = Graphics.FromImage(bmp);

            //        //graph.InterpolationMode = InterpolationMode.High;
            //        //graph.CompositingQuality = CompositingQuality.HighQuality;
            //        //graph.SmoothingMode = SmoothingMode.AntiAlias;

            //        var scaleWidth = (int)(src.Width * scale);
            //        var scaleHeight = (int)(src.Height * scale);

            //        graph.DrawImage(src, ((int)width - scaleWidth) / 2, ((int)height - scaleHeight) / 2, scaleWidth, scaleHeight);
            //        bmp.Save(target);
            //    }

            //}
            //return false;
        }
    }

    public enum ImageSize
    {
        [Description("/Thump/Size75")]
        VerySmall = 1,

        [Description("/Thump/Size150")]
        Small = 3,

        [Description("/Thump/Size300")]
        Medium = 5,

        [Description("/Thump/Size600")]
        Large = 7,


    }
}
