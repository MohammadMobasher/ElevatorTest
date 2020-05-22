using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Repos.User
{
    public class UserAddressRepository : GenericRepository<UserAddress>
    {
        public UserAddressRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }


    }
}
