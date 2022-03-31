using Medium.Desings.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medium.Desings.Database.EntityTypeConfigurations
{
    public class HeaderConfiguration : IEntityTypeConfiguration<Header>
    {
        public void Configure(EntityTypeBuilder<Header> builder)
        {
            builder.HasOne(x => x.Desing).WithOne(x => x.Header).HasForeignKey<Header>(x => x.DesingId);
        }
    }
}
