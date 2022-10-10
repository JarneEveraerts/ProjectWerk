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
    internal class ParkingConfiguration : IEntityTypeConfiguration<Parking>
    {
        public void Configure(EntityTypeBuilder<Parking> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable("Parking");
            builder.HasOne(e => e.Visitor).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Employee).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}