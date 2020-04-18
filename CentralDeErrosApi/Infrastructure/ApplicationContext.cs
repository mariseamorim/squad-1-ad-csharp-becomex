using CentralDeErrosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CentralDeErrosApi.Infrastrutura
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> builder) : base(builder)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<LogErrorOccurrence> LogErrorOccurrence { get; set; }
        public DbSet<Situation> Situations { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Environment> Environments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Server=LPJOI_0007\SQLEXPRESS;Database=Central_De_Erros;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogErrorOccurrence>().HasKey(e => e.ErrorId);
        }
    }
}
