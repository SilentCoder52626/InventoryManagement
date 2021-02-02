using InventoryLibrary.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using InventoryLibrary.Source.Entity;

namespace InventoryLibrary.Infrastructure.Mapping
{
    public class UnitConfiguration: IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> modelBuilder)
        {
            modelBuilder
               .ToTable("unit")
               .HasKey(c => c.Id)
               .HasName("id");

            modelBuilder
                .ToTable("unit")
                .Property(c => c.Name)
                .HasColumnName("name");
            modelBuilder
                .ToTable("unit")
                .Property(c => c.Status)
                .HasColumnName("status");
            modelBuilder
                .ToTable("unit")
                .HasMany(a => a.Items)
                .WithOne(b => b.Unit);
        }
    }
}
