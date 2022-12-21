using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Data.Mappers
{
    internal class ParkingSpotConfiguration : IEntityTypeConfiguration<ParkingSpot>
    {
        public void Configure(EntityTypeBuilder<ParkingSpot> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable("ParkingSpot");
            builder.HasOne(e => e.Visitor).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Employee).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Reserved).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}