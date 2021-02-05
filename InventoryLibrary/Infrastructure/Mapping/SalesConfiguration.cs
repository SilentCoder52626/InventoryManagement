using InventoryLibrary.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using InventoryLibrary.Source.Entity;

namespace InventoryLibrary.Infrastructure.Mapping
{
    public class SalesConfiguration:IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> modelBuilder)
        {

            modelBuilder
                .ToTable("sales")
                .HasKey(c => c.SaleId)
                .HasName("SaleId");

            modelBuilder
                .ToTable("sales")
                .Property(p => p.CusId)
                .HasColumnName("CusId");

            modelBuilder
                .ToTable("sales")
                .Property(c => c.total)
                .HasColumnName("total");
            
            modelBuilder
                .ToTable("sales")
                .Property(c => c.discount)
                .HasColumnName("discount");
            
            modelBuilder
                .ToTable("sales")
                .Property(c => c.netTotal)
                .HasColumnName("netTotal");
            
            modelBuilder
                .ToTable("sales")
                .Property(c => c.SalesDate)
                .HasColumnName("timeStamp");
            modelBuilder
                .ToTable("sales")
                .HasMany(a => a.SalesDetails)
                .WithOne(b => b.Sales);

        }
    }
}
