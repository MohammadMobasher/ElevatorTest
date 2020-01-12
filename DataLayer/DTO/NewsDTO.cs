using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO
{
    public class NewsDTO
    {

        public int Id { get; set; }

        /// <summary>
        /// عنوان مربوط به هر شرط
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// توضیحات خبر
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// خلاصه خبر
        /// </summary>
        public string SummeryNews { get; set; }


        /// <summary>
        /// زمان ثبت خبر
        /// </summary>
        public DateTime Date { get; set; }


        /// <summary>
        /// تعداد بازدید مربو به این خبر
        /// </summary>
        public int ViewCount { get; set; } = 0;

        public string ImageAddress { get; set; }


        /// <summary>
        /// برچسب‌های خبر
        /// </summary>
        public string Tags { get; set; }
    }
}
