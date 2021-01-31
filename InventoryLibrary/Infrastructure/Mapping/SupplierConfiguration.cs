using InventoryLibrary.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLibrary.Infrastructure.Mapping
{
    public class SupplierConfiguration: IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> modelBuilder)
        {
            modelBuilder
                .ToTable("suppliers")
                .HasKey(c => c.Id)
                .HasName("id");

            modelBuilder
                .ToTable("suppliers")
                .Property(c => c.Name)
                .HasColumnName("name");
            modelBuilder
                .ToTable("suppliers")
                .Property(c => c.Address)
                .HasColumnName("address");
            modelBuilder
                .ToTable("suppliers")
                .Property(c => c.Email)
                .HasColumnName("email");
            modelBuilder
                .ToTable("suppliers")
                .Property(c => c.Phone)
                .HasColumnName("phone");
            modelBuilder
                .ToTable("suppliers")
                .Property(c => c.Status)
                .HasColumnName("status");
        }
    }
}
