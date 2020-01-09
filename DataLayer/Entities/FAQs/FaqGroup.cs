using DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entities.FAQs
{
    public class FaqGroup : BaseEntity<int>
    {

        public string Title { get; set; }

        /// <summary>
        /// آیکون مربوط به این گروه
        /// برای نمایش در صفحه موجود در سایت
        /// </summary>
        [StringLength(60)]
        public string Icon { get; set; }

    }
}
