using Medium.Desings.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medium.Desings.Database.EntityTypeConfigurations
{
    public class HeaderNameTextConfiguration : IEntityTypeConfiguration<HeaderNameText>
    {
        public void Configure(EntityTypeBuilder<HeaderNameText> builder)
        {
            builder.HasOne(x => x.HeaderName).WithOne(x => x.Text).HasForeignKey<HeaderNameText>(x => x.HeaderNameId);
        }
    }
}
