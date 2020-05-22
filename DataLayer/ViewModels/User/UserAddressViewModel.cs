using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.User
{
    public class UserAddressViewModel
    {
        public int UserId { get; set; }

        public string Telephone { get; set; }

        public string Plaque { get; set; }

        public string Floor { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string Address { get; set; }

    }
}
