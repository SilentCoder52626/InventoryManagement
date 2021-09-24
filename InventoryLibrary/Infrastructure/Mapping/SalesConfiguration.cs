using InventoryLibrary.Source.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryLibrary.Infrastructure.Mapping
{
    public class SalesConfiguration : IEntityTypeConfiguration<Sale>
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
                .Property(c => c.paidAmount)
                .HasColumnName("paidAmount");
            modelBuilder
                .ToTable("sales")
                .Property(c => c.returnAmount)
                .HasColumnName("returnAmount");
            modelBuilder
                .ToTable("sales")
                .Property(c => c.dueAmount)
                .HasColumnName("dueAmount");
            modelBuilder
                .ToTable("sales")
                .Property(c => c.SalesDate)
                .HasColumnName("timeStamp");
            modelBuilder
                .ToTable("sales")
                .HasMany(a => a.SalesDetails)
                .WithOne()
                .HasForeignKey(a => a.SaleId);
            modelBuilder
                .ToTable("sales")
                .HasOne(a => a.customer)
                .WithMany()
                .HasForeignKey(x => x.CusId);

        }
    }
}
