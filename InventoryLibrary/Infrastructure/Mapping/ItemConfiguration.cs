using InventoryLibrary.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLibrary.Infrastructure.Mapping
{
    public class ItemConfiguration: IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> modelBuilder)
        {
            modelBuilder
               .ToTable("items")
               .HasKey(c => c.Id)
               .HasName("id");

            modelBuilder
                .ToTable("items")
                .Property(c => c.Name)
                .HasColumnName("name");
            modelBuilder
                .ToTable("items")
                .Property(c => c.Status)
                .HasColumnName("status");
        }
    }
}
