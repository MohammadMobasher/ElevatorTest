using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestEntity
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DbContexts>
    {
        public DbContexts CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder();

            builder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Elevator;Data Source=.");

            return new DbContexts(builder.Options);
        }
    }
}
