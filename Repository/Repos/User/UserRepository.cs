using DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repos.User
{
    public class UserRepository : GenericRepository<Users>
    {
        public UserRepository(DatabaseContext db) : base(db)
        {

        }


    }
}
