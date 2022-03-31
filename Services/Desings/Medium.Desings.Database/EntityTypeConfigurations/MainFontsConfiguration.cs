using Medium.Desings.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medium.Desings.Database.EntityTypeConfigurations
{
    public class MainFontsConfiguration : IEntityTypeConfiguration<MainFonts>
    {
        public void Configure(EntityTypeBuilder<MainFonts> builder)
        {
            builder.HasOne(x => x.Desing).WithOne(x => x.Fonts).HasForeignKey<MainFonts>(x => x.DesingId);
        }
    }
}
