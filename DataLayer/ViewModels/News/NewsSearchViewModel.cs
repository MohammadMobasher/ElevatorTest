using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.News
{
    public class NewsSearchViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string SummeryNews { get; set; }
        public DateTime Date { get; set; }
        public string Tags { get; set; }
        public int NewsGroupId { get; set; }
        /// <summary>
        /// یک خبر فعال هست یا خیر
        /// </summary>
        public bool IsActive { get; set; }
    }
}
