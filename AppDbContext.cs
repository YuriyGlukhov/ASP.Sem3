using ASP.Seminar3.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.Seminar3
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Storage> Storages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductStorage> ProductStorages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(en =>
            {
                en.ToTable("Products");

                en.HasKey(x => x.Id).HasName("ProductID");
                en.HasIndex(x => x.Name).IsUnique();

                en.Property(e => e.Name)
                  .HasColumnName("ProductName")
                  .HasMaxLength(255)
                  .IsRequired();

                en.Property(e => e.Description)
                  .HasColumnName("Description")
                  .HasMaxLength(255)
                  .IsRequired();

                en.Property(e => e.Cost)
                  .HasColumnName("Cost")
                  .IsRequired();

                en.Property(e => e.StorageId)
                  .HasColumnName("StorageId")
                  .IsRequired();

                en.HasOne(x => x.Category)
                  .WithMany(c => c.Products)
                  .HasForeignKey(x => x.CategoryId)
                  .HasConstraintName("CategoryToProduct");
            });

            modelBuilder.Entity<Category>(en =>
            {
                en.ToTable("Categories");

                en.HasKey(x => x.Id).HasName("CategoryID");
                en.HasIndex(x => x.Name).IsUnique();

                en.Property(e => e.Name)
                  .HasColumnName("CategoryName")
                  .HasMaxLength(255)
                  .IsRequired();

                en.Property(e => e.Description)
                  .HasColumnName("Description")
                  .HasMaxLength(255)
                  .IsRequired();
            });

            modelBuilder.Entity<Storage>(en =>
            {
                en.ToTable("Storages");

                en.HasKey(x => x.Id).HasName("StorageId");

                en.Property(en => en.Name)
                  .HasColumnName("StorageName")
                  .HasMaxLength(255)
                  .IsRequired();

                en.Property(en => en.Count)
                  .HasColumnName("Count")
                  .IsRequired();
            });


            modelBuilder.Entity<ProductStorage>()
                .HasKey(ps => new { ps.ProductId, ps.StorageId });

            modelBuilder.Entity<ProductStorage>()
                .HasOne(ps => ps.Product)
                .WithMany(p => p.ProductStorages)
                .HasForeignKey(ps => ps.ProductId);

            modelBuilder.Entity<ProductStorage>()
                .HasOne(ps => ps.Storage)
                .WithMany(s => s.ProductStorages)
                .HasForeignKey(ps => ps.StorageId);
        }

    }
}
