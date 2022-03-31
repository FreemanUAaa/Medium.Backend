using Medium.Desings.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medium.Desings.Database.EntityTypeConfigurations
{
    public class HeaderImageConfiguration : IEntityTypeConfiguration<HeaderImage>
    {
        public void Configure(EntityTypeBuilder<HeaderImage> builder)
        {
            builder.HasOne(x => x.Header).WithOne(x => x.Image).HasForeignKey<HeaderImage>(x => x.HeaderId);
        }
    }
}
