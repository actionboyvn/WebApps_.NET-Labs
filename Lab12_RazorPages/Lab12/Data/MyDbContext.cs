using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab12.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab12.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
