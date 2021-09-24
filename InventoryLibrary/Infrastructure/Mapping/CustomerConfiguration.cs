using InventoryLibrary.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryLibrary.Infrastructure.Mapping
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> modelBuilder)
        {
            modelBuilder
                .ToTable("customers")
                .HasKey(c => c.CusId)
                .HasName("CusId");

            modelBuilder
                .ToTable("customers")
                .Property(c => c.FullName)
                .HasColumnName("FullName");
            modelBuilder
                .ToTable("customers")
                .Property(c => c.Address)
                .HasColumnName("Address");
            modelBuilder
                .ToTable("customers")
                .Property(c => c.Email)
                .HasColumnName("Email");
            modelBuilder
                .ToTable("customers")
                .Property(c => c.PhoneNumber)
                .HasColumnName("PhoneNumber");
            modelBuilder
                .ToTable("customers")
                .Property(c => c.Gender)
                .HasColumnName("Gender");
        }
    }
}
