﻿using Microsoft.EntityFrameworkCore;
using Allphi.Models;
using System.Reflection;

namespace Allphi.Data
{
    internal class AllphiContect : DbContext
    {
        public DbSet<Business> Business { get; set; }
        public DbSet<Contract> Contract { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Visit> Visit { get; set; }
        public DbSet<Visitor> Visitor { get; set; }
        public DbSet<Parking> Parking { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\sqlexpress;Initial Catalog=Allphi;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}