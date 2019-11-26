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
        public int NewsGroupId { get; set; }


        /// <summary>
        /// فایلی که ارسال شده برای ذخیره
        /// </summary>
        public IFormFile ImageFile { get; set; }

        /// <summary>
        /// شماره کاربری کسی در حال ذخیره خبر است
        /// </summary>
        public int UserId { get; set; }


        /// <summary>
        /// آدرسی که فایل مورد نظر ذخیره شده است
        /// </summary>
        public string ImageAddress { get; set; }
    }
}
