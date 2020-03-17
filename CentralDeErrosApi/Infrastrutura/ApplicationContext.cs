using CentralDeErrosApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErrosApi.Infrastrutura
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions builder) : base(builder)
        {

        }

        public DbSet<Error> Errors { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<ErrorOccurrence> ErrorOccurrences { get; set; }
        public DbSet<Situation> Situations { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Ambiente> Ambiente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {       
            modelBuilder.Entity<Situation>().HasMany(s => s.ErrorOccurrences).WithOne(s => s.Situation).IsRequired();
            modelBuilder.Entity<Level>().HasMany(l => l.Errors).WithOne(l => l.Level).IsRequired();
            modelBuilder.Entity<Ambiente>().HasMany(e => e.Errors).WithOne(e => e.Ambiente).IsRequired();
            
        }
    }
}
