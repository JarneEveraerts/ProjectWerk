using Allphi.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Allphi.Data
{
    public class AllphiContext : DbContext
    {
        public AllphiContext()
        {
        }

        public AllphiContext(DbContextOptions<AllphiContext> options)
            : base(options)
        {
        }

        public DbSet<Business> Business { get; set; }
        public DbSet<Contract> Contract { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Visit> Visit { get; set; }
        public DbSet<Visitor> Visitor { get; set; }
        public DbSet<Parking> Parking { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Services.Configurator.DbConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}