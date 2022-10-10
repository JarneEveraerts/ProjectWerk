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
    internal class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable("Contract");
            builder.HasOne(e => e.Business);
        }
    }
}