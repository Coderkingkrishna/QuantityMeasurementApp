using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Models.Entities;

namespace QuantityMeasurementApp.Repository
{
    /// <summary>
    /// EF Core DbContext for quantity measurement persistence.
    /// </summary>
    public sealed class QuantityMeasurementDbContext : DbContext
    {
        public QuantityMeasurementDbContext(DbContextOptions<QuantityMeasurementDbContext> options)
            : base(options) { }

        public DbSet<QuantityMeasurementEntity> QuantityMeasurementOperations =>
            Set<QuantityMeasurementEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuantityMeasurementEntity>(entity =>
            {
                // Keep mapping explicit so schema remains stable across migrations.
                entity.ToTable("QuantityMeasurementOperations", "dbo");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Description).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.IsError).IsRequired();
                entity.Property(e => e.ErrorMessage).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.CreatedAt).IsRequired();

                entity.HasIndex(e => e.CreatedAt).HasDatabaseName("IX_QuantityMeasurementOperations_CreatedAt");
            });
        }
    }
}