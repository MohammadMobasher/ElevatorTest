using DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Repos.User
{
    public class UserRepository : GenericRepository<Users>
    {
        public UserRepository(DatabaseContext db) : base(db)
        {

        }

     
    }
}
