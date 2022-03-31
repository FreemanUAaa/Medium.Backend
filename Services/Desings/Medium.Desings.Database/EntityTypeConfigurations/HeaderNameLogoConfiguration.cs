using Medium.Desings.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medium.Desings.Database.EntityTypeConfigurations
{
    public class HeaderNameLogoConfiguration : IEntityTypeConfiguration<HeaderNameLogo>
    {
        public void Configure(EntityTypeBuilder<HeaderNameLogo> builder)
        {
            builder.HasOne(x => x.HeaderName).WithOne(x => x.Logo).HasForeignKey<HeaderNameLogo>(x => x.HeaderNameId);
        }
    }
}
