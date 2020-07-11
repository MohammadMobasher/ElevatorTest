using DataLayer.Entities.Common;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class UserAddress : IEntity
    {
        [Key]
        public int UserId { get; set; }

        public string PhoneNumber { get; set; }

        public string Telephone { get; set; }

        public string Plaque { get; set; }

        public string Floor { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string Address { get; set; }

        public TehranAreas? TehranAreasFrom { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual Users.Users Users { get; set; }

    }
}
