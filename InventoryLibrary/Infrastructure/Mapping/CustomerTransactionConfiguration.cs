using InventoryLibrary.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryLibrary.Infrastructure.Mapping
{
    public class CustomerTransactionConfiguration : IEntityTypeConfiguration<CustomerTransaction>
    {
        public void Configure(EntityTypeBuilder<CustomerTransaction> modelBuilder)
        {

            modelBuilder
                .ToTable("customer_transaction")
                .HasKey(c => c.Id)
                .HasName("id");

            modelBuilder
                .ToTable("customer_transaction")
                .Property(p => p.CustomerId)
                .HasColumnName("customer_id");

            modelBuilder
                .ToTable("customer_transaction")
                .Property(c => c.Amount)
                .HasColumnName("amount");

            modelBuilder
                .ToTable("customer_transaction")
                .Property(c => c.AmountType)
                .HasColumnName("amount_type");

            modelBuilder
                .ToTable("customer_transaction")
                .Property(c => c.Type)
                .HasColumnName("type");
            modelBuilder
                .ToTable("customer_transaction")
                .Property(c => c.TransactionDate)
                .HasColumnName("transaction_date");
            modelBuilder
               .ToTable("customer_transaction")
               .Property(c => c.ExtraId)
               .HasColumnName("extraId");

            modelBuilder
                .ToTable("customer_transaction")
                .HasOne(a => a.Customer)
                .WithMany()
                .HasForeignKey(p => p.CustomerId);

        }
    }
}
