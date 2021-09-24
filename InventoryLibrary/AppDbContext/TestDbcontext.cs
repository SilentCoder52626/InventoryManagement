using InventoryLibrary.Entity;
using InventoryLibrary.Infrastructure.Mapping;
using InventoryLibrary.Source.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InventoryLibrary.AppDbContext
{
    public class Testdbcontext : DbContext
    {
        private readonly IConfiguration _configuration;
        public Testdbcontext(DbContextOptions<Testdbcontext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetails> sale_details { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetail> purchase_details { get; set; }
        public DbSet<CustomerTransaction> Customer_Transaction { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new UnitConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseDetailConfiguration());
            modelBuilder.ApplyConfiguration(new SalesConfiguration());
            modelBuilder.ApplyConfiguration(new SalesDetailConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());



        }
    }
}
