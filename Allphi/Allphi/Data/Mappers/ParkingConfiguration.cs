using Allphi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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