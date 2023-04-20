using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTask1.Models;

namespace TestTask1.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext (DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<City> City { get; set; }
        public DbSet<Flat> Flat { get; set; }
        public DbSet<House> House { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<Street> Street { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<Flat>().ToTable("Flat");
            modelBuilder.Entity<House>().ToTable("House");
            modelBuilder.Entity<Owner>().ToTable("Owner");
            modelBuilder.Entity<Street>().ToTable("Street");
        }
    }
}