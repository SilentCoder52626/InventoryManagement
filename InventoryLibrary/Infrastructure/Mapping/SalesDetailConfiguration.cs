using InventoryLibrary.Source.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using InventoryLibrary.Entity;

namespace InventoryLibrary.Infrastructure.Mapping
{
    public class SalesDetailConfiguration: IEntityTypeConfiguration<SaleDetails>
    {
        public void Configure(EntityTypeBuilder<SaleDetails> modelBuilder)
        {

            modelBuilder
                .ToTable("sale_details")
                .HasKey(k => k.SaleDetailId)
                .HasName("SaleDetailId");
            
            modelBuilder
                .ToTable("sale_details")
                .Property(p => p.ItemName)
                .HasColumnName("ItemName");
            modelBuilder
                .ToTable("sale_details")
                .Property(p => p.SaleId)
                .HasColumnName("SaleId");
            modelBuilder
                .ToTable("sale_details")
                .Property(p => p.Total)
                .HasColumnName("Total");
            modelBuilder
                .ToTable("sale_details")
                .Property(p => p.Qty)
                .HasColumnName("qty");
            modelBuilder
                .ToTable("sale_details")
                .Property(p => p.ItemId)
                .HasColumnName("item_id");
            modelBuilder
                .ToTable("sale_details")
                .Property(p => p.Price)
                .HasColumnName("Price");
            modelBuilder
                .ToTable("sale_details")
                .HasOne(a => a.Sales)
                .WithMany(a => a.SalesDetails);

        }
    }
}
