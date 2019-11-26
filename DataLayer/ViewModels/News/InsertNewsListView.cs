using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.News
{
    public class InsertNewsListView
    {

        /// <summary>
        /// عنوان مربوط به هر خبر
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// متن خبر
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// خلاصه خبر
        /// </summary>
        public string SummeryNews { get; set; }


        /// <summary>
        /// برچسب‌های خبر
        /// </summary>
        public string Tags { get; set; }


        /// <summary>
        /// دسته‌بندی خبر
        /// </summary>
        public int NewsGroup { get; set; }


        //TODO
        public IFormFile ImageAddress { get; set; }
    }
}
