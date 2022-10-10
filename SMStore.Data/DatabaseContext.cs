using Microsoft.EntityFrameworkCore;
using SMStore.Entities;
using System.Reflection;

namespace SMStore.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localDB)\MSSQLLocalDB; Database=SMStore; Trusted_Connection=True;") ;
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FluentAPI : Veritabanı tablo ve kolonlarını oluşturmak için data annotations a alternatif olarak kullanılabilir bir teknoloji
            modelBuilder.Entity<AppUser>().Property(a => a.Name).HasColumnType("varchar(50)").HasMaxLength(50);
            modelBuilder.Entity<AppUser>().Property(a => a.Surname).HasColumnType("varchar(50)").HasMaxLength(50);
            modelBuilder.Entity<AppUser>().Property(a => a.Email).HasColumnType("varchar(50)").HasMaxLength(50);
            modelBuilder.Entity<AppUser>().Property(a => a.Phone).HasColumnType("varchar(15)");
            modelBuilder.Entity<AppUser>().Property(a => a.Username).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<AppUser>().Property(a => a.Password).HasColumnType("nvarchar(50)");

            //FluentAPI ile Veritabanı oluştuktan sonra ilk kaydı ekleme
            modelBuilder.Entity<AppUser>().HasData( 
            
               new AppUser
               {
                   Id = 1,  
                   Email = "admin@smstore.com",
                   IsActive = true,
                   IsAdmin = true,
                   Name = "Admin",
                   Surname = "User",
                   Password = "123"

               }

            );
            // Configurations altındaki class ları burada tanımlamamız gerekiyor.
          
           // modelBuilder.ApplyConfiguration(new BrandConfiguration());  // Configuration class larını bu şekilde tek tek çağırabiliriz.
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Configuration class larını bu şekilde topluca da ekleyebiliriz.

            base.OnModelCreating(modelBuilder);

        }


    }
}
