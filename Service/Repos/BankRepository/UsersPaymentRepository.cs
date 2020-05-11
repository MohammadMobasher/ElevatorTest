using DataLayer.Entities.Bank;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Repos.BankRepository
{
    public class UsersPaymentRepository : GenericRepository<UsersPayment>
    {
        public UsersPaymentRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
