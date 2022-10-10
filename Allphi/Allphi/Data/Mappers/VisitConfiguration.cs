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
    public class VisitConfiguration : IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("Visit");

            builder.HasOne(e => e.Visitor).WithOne().HasForeignKey<Visit>();

            builder.HasOne(e => e.Business).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Employee).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}