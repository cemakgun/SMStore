using Microsoft.EntityFrameworkCore;
using SMStore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SMStore.Data.Configurations
{
   public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Surname).HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(50);
            builder.Property(x => x.Phone).HasMaxLength(15);
            builder.Property(x => x.Password).HasMaxLength(500);
        }
    }
}
