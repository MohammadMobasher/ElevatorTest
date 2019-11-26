using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestEntity
{
    public class DbContexts:DbContext
    {
        public DbContexts(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Test> Test { get; set; }
    }
}
