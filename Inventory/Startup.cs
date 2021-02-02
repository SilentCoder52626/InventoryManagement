using Inventory.Repository.Implementation;
using InventoryLibrary.AppDbContext;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Services.Implementation;
using InventoryLibrary.Services.ServiceInterface;
using InventoryLibrary.Source.Repository;
using InventoryLibrary.Source.Repository.Interface;
using InventoryLibrary.Source.Services;
using InventoryLibrary.Source.Services.Implementation;
using InventoryLibrary.Source.Services.ServiceInterface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NToastNotify;

namespace Inventory
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddDbContext<Testdbcontext>(a =>
            a.UseMySql(Configuration.GetConnectionString("TestConnection")).UseLazyLoadingProxies());

            services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
            {
                ProgressBar = false,
                PositionClass = ToastPositions.BottomFullWidth
            });

            services.AddScoped<CustomerServiceInterface, CustomerService>();
            services.AddScoped<CustomerRepositoryInterface, CustomerRepository>();

            services.AddScoped<ItemRepositoryInterface, ItemRepository>();
            services.AddScoped<ItemServiceInterface, ItemService>();
            
            services.AddScoped<UnitRepositoryInterface, UnitRepository>();
            services.AddScoped<UnitServiceInterface, UnitService>();

            services.AddScoped<SaleRepositoryInterface, SaleRepository>();
            services.AddScoped<SaleServiceInterface, SaleService>();

            services.AddScoped<SaleDetailRepositoryInterface, SaleDetailRepository>();

            services.AddScoped<SupplierRepositoryInterface, SupplierRepository>();
            services.AddScoped<SupplierServiceInterface, SupplierService>();

            services.AddScoped<PurchaseRepositoryInterface, PurchaseRepository>();
            services.AddScoped<PurchaseServiceInterface, PurchaseService>();

            services.AddScoped<PurchaseDetailRepositoryInterface, PurchaseDetailRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseNToastNotify();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
