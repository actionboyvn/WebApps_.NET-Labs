using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab102.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab102.Data
{
    public class Context: IdentityDbContext
    {
        public Context(DbContextOptions options): base(options)
        {

        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // create tables for Identity
            //modelBuilder.Seed();
        }
    }
}
