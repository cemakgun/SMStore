using Microsoft.EntityFrameworkCore;
using SMStore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace SMStore.Data.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Image).HasMaxLength(100);
        }
    }
}
