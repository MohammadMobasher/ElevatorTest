using Core.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO
{
    public class NewsGroupDTO
    {
        [ColumnGridViewAttribute(AlignCenter =true,  DisplayName = "شماره", Searchable = false, Sortable = false, Width = "30px")]
        public int Id { get; set; }

        [ColumnGridViewAttribute(AlignCenter = true, DisplayName = "عنوان گروه", Searchable = true, Sortable = true, Width = "100px")]
        public string Title { get; set; }
    }
}
