using Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Pesistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) :  base(options)
        {

        }

        public DbSet<Value> Values { get; set; }
    }
}
