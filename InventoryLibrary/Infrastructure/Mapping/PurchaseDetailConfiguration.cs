using InventoryLibrary.Source.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryLibrary.Infrastructure.Mapping
{
    public class PurchaseDetailConfiguration : IEntityTypeConfiguration<PurchaseDetail>
    {
        public void Configure(EntityTypeBuilder<PurchaseDetail> modelBuilder)
        {

            modelBuilder
                .ToTable("purchase_details")
                .HasKey(k => k.Id)
                .HasName("id");

            modelBuilder
                .ToTable("purchase_details")
                .HasOne(a => a.Purchase)
                .WithMany(b => b.PurchaseDetails)
                .HasForeignKey(a => a.PurchaseId);

            modelBuilder
                .ToTable("purchase_details")
                .Property(p => p.ItemId)
                .HasColumnName("item_id");
            modelBuilder
                .ToTable("purchase_details")
                .Property(p => p.PurchaseId)
                .HasColumnName("purchase_id");
            modelBuilder
                .ToTable("purchase_details")
                .Property(p => p.Qty)
                .HasColumnName("qty");
            modelBuilder
                .ToTable("purchase_details")
                .Property(p => p.Rate)
                .HasColumnName("rate");
            modelBuilder
                .ToTable("purchase_details")
                .Property(p => p.SalesRate)
                .HasColumnName("sales_rate");

            modelBuilder
                .ToTable("purchase_details")
                .HasOne(p => p.Items)
                .WithMany()
                .HasForeignKey(a => a.ItemId);

        }
    }
}
