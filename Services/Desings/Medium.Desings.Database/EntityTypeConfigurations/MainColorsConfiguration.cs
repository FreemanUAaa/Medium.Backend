using Medium.Desings.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medium.Desings.Database.EntityTypeConfigurations
{
    public class MainColorsConfiguration : IEntityTypeConfiguration<MainColors>
    {
        public void Configure(EntityTypeBuilder<MainColors> builder)
        {
            builder.HasOne(x => x.Desing).WithOne(x => x.Colors).HasForeignKey<MainColors>(x => x.DesingId);
        }
    }
}
