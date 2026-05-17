using APBD_9.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_9.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<PC> PCs { get; set; }
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<PCComponent> PCComponents { get; set; }
        public virtual DbSet<ComponentType> ComponentTypes { get; set; }
        public virtual DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DB9;Trusted_Connection=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComponentType>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasData(
                    new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Central Processing Unit" },
                    new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Processing Unit" },
                    new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Random Access Memory" }
                );
            });

            modelBuilder.Entity<ComponentManufacturer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.FoundationDate)
                    .IsRequired();

                entity.HasData(
                    new ComponentManufacturer { Id = 1, Abbreviation = "Intel", FullName = "Intel Corporation", FoundationDate = new DateOnly(1968, 7, 18) },
                    new ComponentManufacturer { Id = 2, Abbreviation = "AMD", FullName = "Advanced Micro Devices", FoundationDate = new DateOnly(1969, 5, 1) },
                    new ComponentManufacturer { Id = 3, Abbreviation = "NVIDIA", FullName = "NVIDIA Corporation", FoundationDate = new DateOnly(1993, 4, 5) }
                );
            });

            modelBuilder.Entity<Component>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnType("char(10)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Description)
                    .HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.HasOne(e => e.ComponentManufacturer)
                    .WithMany(m => m.Components)
                    .HasForeignKey(e => e.ComponentManufacturersId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ComponentType)
                    .WithMany(t => t.Components)
                    .HasForeignKey(e => e.ComponentTypesId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasData(
                    new Component { Code = "CPU-I9-14", Name = "Intel Core i9-14900K", Description = "High-end desktop CPU", ComponentManufacturersId = 1, ComponentTypesId = 1 },
                    new Component { Code = "GPU-RTX40", Name = "NVIDIA GeForce RTX 4090", Description = "Flagship GPU", ComponentManufacturersId = 3, ComponentTypesId = 2 },
                    new Component { Code = "RAM-DDR5A", Name = "AMD EXPO DDR5 32GB", Description = "High-speed DDR5 kit", ComponentManufacturersId = 2, ComponentTypesId = 3 }
                );
            });

            modelBuilder.Entity<PC>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Weight)
                    .HasColumnType("float(5)");

                entity.Property(e => e.Warranty)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                entity.Property(e => e.Stock)
                    .IsRequired();

                entity.HasData(
                    new PC { Id = 1, Name = "Gaming Beast X", Weight = 12.5f, Warranty = 36, CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0), Stock = 5 },
                    new PC { Id = 2, Name = "Office Mini Pro", Weight = 4.2f, Warranty = 24, CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0), Stock = 12 },
                    new PC { Id = 3, Name = "Workstation Ultra", Weight = 18.0f, Warranty = 48, CreatedAt = new DateTime(2026, 3, 1, 8, 0, 0), Stock = 3 }
                );
            });

            modelBuilder.Entity<PCComponent>(entity =>
            {
                entity.HasKey(e => new { e.PCId, e.ComponentCode });

                entity.Property(e => e.ComponentCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnType("char(10)");

                entity.Property(e => e.Amount)
                    .IsRequired();

                entity.HasOne(e => e.PC)
                    .WithMany(p => p.PCComponents)
                    .HasForeignKey(e => e.PCId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Component)
                    .WithMany(c => c.PCComponents)
                    .HasForeignKey(e => e.ComponentCode)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasData(
                    new PCComponent { PCId = 1, ComponentCode = "CPU-I9-14", Amount = 1 },
                    new PCComponent { PCId = 1, ComponentCode = "GPU-RTX40", Amount = 2 },
                    new PCComponent { PCId = 2, ComponentCode = "RAM-DDR5A", Amount = 2 },
                    new PCComponent { PCId = 3, ComponentCode = "CPU-I9-14", Amount = 2 },
                    new PCComponent { PCId = 3, ComponentCode = "RAM-DDR5A", Amount = 4 }
                );
            });
        }
    }
}
