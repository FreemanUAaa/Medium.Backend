using Medium.Desings.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medium.Desings.Database.EntityTypeConfigurations
{
    public class HeaderColorsConfiguration : IEntityTypeConfiguration<HeaderColors>
    {
        public void Configure(EntityTypeBuilder<HeaderColors> builder)
        {
            builder.HasOne(x => x.Header).WithOne(x => x.Colors).HasForeignKey<HeaderColors>(x => x.HeaderId);
        }
    }
}
