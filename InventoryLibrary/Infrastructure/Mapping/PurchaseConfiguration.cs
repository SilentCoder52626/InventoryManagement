using InventoryLibrary.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLibrary.Infrastructure.Mapping
{
    public class PurchaseConfiguration:IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> modelBuilder)
        {

            modelBuilder
                .ToTable("purchases")
                .HasKey(c => c.Id)
                .HasName("id");

            modelBuilder
                .ToTable("purchases")
                .Property(p => p.SupplierId)
                .HasColumnName("supplier_id");

            modelBuilder
                .ToTable("purchases")
                .Property(c => c.Total)
                .HasColumnName("total");
            
            modelBuilder
                .ToTable("purchases")
                .Property(c => c.Discount)
                .HasColumnName("discount");
            
            modelBuilder
                .ToTable("purchases")
                .Property(c => c.GrandTotal)
                .HasColumnName("grand_total");
            
            modelBuilder
                .ToTable("purchases")
                .Property(c => c.PurchaseDateTime)
                .HasColumnName("date_time");
            modelBuilder
                .ToTable("purchases")
                .HasMany(a => a.PurchaseDetails)
                .WithOne(b => b.Purchase);

        }
    }
}
