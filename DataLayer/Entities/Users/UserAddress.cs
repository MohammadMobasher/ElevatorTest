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
        public int Id { get; set; }

        public int UserId { get; set; }

        /// <summary>
        /// شماره فاکتور مورد نظر
        /// </summary>
        public int? ShopOrderId { get; set; }

        /// <summary>
        /// نام گیرنده
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// شماره موبایل تحویل گیرنده
        /// </summary>
        public string PhoneNumberTo { get; set; }
        public string Title { get; set; }

        public string PhoneNumber { get; set; }

        public string Telephone { get; set; }

        public string Plaque { get; set; }

        public string Floor { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string Address { get; set; }

        public TehranAreas? TehranAreasFrom { get; set; }

        /// <summary>
        /// آیا خارج از تهران می باشد
        /// </summary>
        public bool IsOutTehran { get; set; }

        /// <summary>
        /// نام شهر
        /// </summary>
        public string Province { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual Users.Users Users { get; set; }



        [ForeignKey(nameof(ShopOrderId))]
        public virtual ShopOrder ShopOrder { get; set; }
    }
}
