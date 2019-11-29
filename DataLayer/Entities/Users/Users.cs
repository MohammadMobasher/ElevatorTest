﻿using DataLayer.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities.Users
{
    public class Users : IdentityUser<int>, IEntity
    {
        public Users()
        {
            CreateDate = DateTime.Now;
        }

        public DateTime? CreateDate { get; set; }
        public bool IsActive { get; set; }
    }
}