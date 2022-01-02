using InventoryLibrary.Entity;
using InventoryLibrary.Source.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryLibrary.Infrastructure.Mapping
{
    public class AuditConfiguration : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> modelBuilder)
        {
            modelBuilder
                .ToTable("audits")
                .HasKey(c => c.Id)
                .HasName("id");

            //modelBuilder
            //    .ToTable("audits")
            //    .Property(c => c.UserId)
            //    .HasColumnName("user_id");
            modelBuilder
                .ToTable("audits")
                .Property(c => c.Type)
                .HasColumnName("type");
            modelBuilder
                .ToTable("audits")
                .Property(c => c.TableName)
                .HasColumnName("table_name");
            modelBuilder
                .ToTable("audits")
                .Property(c => c.DateTime)
                .HasColumnName("date_time");
            modelBuilder
                .ToTable("audits")
                .Property(c => c.OldValues)
                .HasColumnName("old_values");
            modelBuilder
                .ToTable("audits")
                .Property(c => c.NewValues)
                .HasColumnName("new_values");
            modelBuilder
                .ToTable("audits")
                .Property(c => c.AffectedColumns)
                .HasColumnName("affected_columns");
            modelBuilder
                .ToTable("audits")
                .Property(c => c.PrimaryKey)
                .HasColumnName("primary_key");
        }
    }
}
