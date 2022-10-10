using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Allphi.Models;

namespace Allphi.Data.Mappers
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable("Employee");
            builder.HasOne(e => e.Business).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}