using Allphi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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