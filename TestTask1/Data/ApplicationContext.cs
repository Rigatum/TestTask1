using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTask1.Models.Domain;

namespace TestTask1.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Flat> Flats { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Street> Streets { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<Flat>().ToTable("Flat");
            modelBuilder.Entity<House>().ToTable("House", "FlatsNumber <= 100");
            modelBuilder.Entity<Owner>().ToTable("Owner");
            modelBuilder.Entity<Street>().ToTable("Street");
        }
    }
}