using Microsoft.EntityFrameworkCore;
using SMStore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace SMStore.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Image).HasMaxLength(100);
            builder.Property(x => x.ProductCode).HasMaxLength(30);
            // Burada class lar arasındaki ilişkileri de belirtebiliyoruz.
            builder.HasOne(x => x.Brand).WithMany(x => x.Products).HasForeignKey(b => b.BrandId); // Burada Brand Class ile Product Class i arasında bire çok bir ilişki olacağını belirttik. 1 olan kısım Brand olduğu için "HASONE" özellğine brand i belirttik. Çok olan kısım products olacağı için bunun da WithMany içersinde belirttik. HasForeignKey de ise veritabanında oluşacak kolonlardan BrandId nin foreign key olacağını belirttik.
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(b => b.CategoryId);
        }
    }   
}
