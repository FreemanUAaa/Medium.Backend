using Medium.Desings.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medium.Desings.Database.EntityTypeConfigurations
{
    public class HeaderNameConfiguration : IEntityTypeConfiguration<HeaderName>
    {
        public void Configure(EntityTypeBuilder<HeaderName> builder)
        {
            builder.HasOne(x => x.Header).WithOne(x => x.Name).HasForeignKey<HeaderName>(x => x.HeaderId);
        }
    }
}
