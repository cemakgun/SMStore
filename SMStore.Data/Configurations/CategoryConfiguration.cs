using Microsoft.EntityFrameworkCore;
using SMStore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SMStore.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Image).HasMaxLength(100);
        }
    }
}
