using Microsoft.EntityFrameworkCore;
using RestApi.Api.Models;

namespace RestApi.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<PC> PCs => Set<PC>();
    public DbSet<Component> Components => Set<Component>();
    public DbSet<ComponentType> ComponentTypes => Set<ComponentType>();
    public DbSet<ComponentManufacturer> ComponentManufacturers => Set<ComponentManufacturer>();
    public DbSet<PCComponent> PCComponents => Set<PCComponent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PC>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Weight).HasColumnType("float(5)");
            entity.Property(e => e.Warranty).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.Stock).IsRequired();

            entity.HasData(
                new PC { Id = 1, Name = "My First PC", Weight = 12.5, Warranty = 36, CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0), Stock = 5 },
                new PC { Id = 2, Name = "Old Lab PC", Weight = 4.2, Warranty = 24, CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0), Stock = 12 },
                new PC { Id = 3, Name = "Test Computer", Weight = 6.8, Warranty = 24, CreatedAt = new DateTime(2026, 3, 1, 10, 0, 0), Stock = 8 }
            );
        });

        modelBuilder.Entity<ComponentType>(entity =>
        {
            entity.Property(e => e.Abbreviation).HasMaxLength(30).IsRequired();
            entity.Property(e => e.Name).HasMaxLength(150).IsRequired();

            entity.HasData(
                new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Processor" },
                new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Card" },
                new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Memory" }
            );
        });

        modelBuilder.Entity<ComponentManufacturer>(entity =>
        {
            entity.Property(e => e.Abbreviation).HasMaxLength(30).IsRequired();
            entity.Property(e => e.FullName).HasMaxLength(300).IsRequired();
            entity.Property(e => e.FoundationDate).HasColumnType("date");

            entity.HasData(
                new ComponentManufacturer { Id = 1, Abbreviation = "ChipStore", FullName = "Chip Store", FoundationDate = new DateOnly(2020, 1, 1) },
                new ComponentManufacturer { Id = 2, Abbreviation = "GrInc", FullName = "Graphics Inc", FoundationDate = new DateOnly(2021, 6, 15) },
                new ComponentManufacturer { Id = 3, Abbreviation = "Computronics", FullName = "Computronics", FoundationDate = new DateOnly(2022, 3, 10) }
            );
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code).HasColumnType("char(10)").IsFixedLength().HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(300).IsRequired();
            entity.Property(e => e.Description).HasColumnType("nvarchar(max)");

            entity.HasOne(e => e.ComponentManufacturer)
                .WithMany(m => m.Components)
                .HasForeignKey(e => e.ComponentManufacturersId);

            entity.HasOne(e => e.ComponentType)
                .WithMany(t => t.Components)
                .HasForeignKey(e => e.ComponentTypesId);

            entity.HasData(
                new Component { Code = "CPU0000001", Name = "Basic CPU", Description = "Sample processor for the project", ComponentManufacturersId = 1, ComponentTypesId = 1 },
                new Component { Code = "GPU0000001", Name = "Basic GPU", Description = "Sample graphics card for the project", ComponentManufacturersId = 2, ComponentTypesId = 2 },
                new Component { Code = "RAM0000001", Name = "8GB RAM", Description = "Sample memory stick for the project", ComponentManufacturersId = 3, ComponentTypesId = 3 }
            );
        });

        modelBuilder.Entity<PCComponent>(entity =>
        {
            entity.HasKey(e => new { e.PCId, e.ComponentCode });

            entity.Property(e => e.ComponentCode).HasColumnType("char(10)").IsFixedLength().HasMaxLength(10);
            entity.Property(e => e.Amount).IsRequired();

            entity.HasOne(e => e.PC)
                .WithMany(p => p.PCComponents)
                .HasForeignKey(e => e.PCId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Component)
                .WithMany(c => c.PCComponents)
                .HasForeignKey(e => e.ComponentCode)
                .HasPrincipalKey(c => c.Code);

            entity.HasData(
                new PCComponent { PCId = 1, ComponentCode = "CPU0000001", Amount = 1 },
                new PCComponent { PCId = 1, ComponentCode = "GPU0000001", Amount = 1 },
                new PCComponent { PCId = 1, ComponentCode = "RAM0000001", Amount = 2 },
                new PCComponent { PCId = 2, ComponentCode = "CPU0000001", Amount = 1 },
                new PCComponent { PCId = 2, ComponentCode = "RAM0000001", Amount = 1 },
                new PCComponent { PCId = 3, ComponentCode = "CPU0000001", Amount = 1 },
                new PCComponent { PCId = 3, ComponentCode = "GPU0000001", Amount = 1 },
                new PCComponent { PCId = 3, ComponentCode = "RAM0000001", Amount = 1 }
            );
        });
    }
}
