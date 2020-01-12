using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels
{
    public class DeleteViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// همه جا استفاده نمیشود 
        /// فقط برای بعضی جاها از این فیلد استفاده می شود
        /// فقط برای بعضی جاها از این فیلد استفاده می شود
        /// </summary>
        public int GroupId { get; set; }
    }
}
