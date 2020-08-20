using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.User
{
    public class EditProfileViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string NationalCode { get; set; }

    }

    public class EditProfileUserAddressViewModel
    {
        public int UserId { get; set; }
        public string Telephone { get; set; }

        public string Address { get; set; }

        public string Plaque { get; set; }

        public string ZipCode { get; set; }
    }
}
