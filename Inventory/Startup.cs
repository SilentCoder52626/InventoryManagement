using Inventory.Repository.Implementation;
using InventoryLibrary.AppDbContext;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Services.Implementation;
using InventoryLibrary.Services.ServiceInterface;
using InventoryLibrary.Source.Entity;
using InventoryLibrary.Source.Repository;
using InventoryLibrary.Source.Repository.Interface;
using InventoryLibrary.Source.Services;
using InventoryLibrary.Source.Services.Implementation;
using InventoryLibrary.Source.Services.ServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NToastNotify;
using System;
using System.Threading.Tasks;

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
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<Testdbcontext>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireDigit = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.SignIn.RequireConfirmedEmail = false;

            });


            services.ConfigureApplicationCookie(options =>
            {

                options.Events.OnRedirectToAccessDenied = (evnt) =>
                {
                    if (evnt.Request.Path.StartsWithSegments("/api"))
                    {
                        evnt.Response.StatusCode = 403;
                        evnt.Response.ContentType = "application/json";
                        var data = new
                        {
                            Message = "You are not authorized here",
                            Code = StatusCodes.Status401Unauthorized,
                            Status = "401 Unauthorized",
                            Errors = "Unauthorized",
                            LoginRedirectUrl = "/Error/AccessDenied"
                        };
                        evnt.Response.WriteAsync(JsonConvert.SerializeObject(data));
                        return Task.CompletedTask;
                    }
                    else
                    {
                        evnt.Response.Redirect("/Error/AccessDenied");
                    }

                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToLogin = (evnt) =>
                {
                    if (evnt.Request.Path.StartsWithSegments("/api"))
                    {
                        evnt.Response.StatusCode = 401;
                        evnt.Response.ContentType = "application/json";
                        var data = new
                        {
                            Message = "You are not authorized here",
                            Code = StatusCodes.Status401Unauthorized,
                            Status = "401 Unauthorized",
                            Errors = "Unauthorized",
                            LoginRedirectUrl = "/Account/Login"
                        };
                        evnt.Response.WriteAsync(JsonConvert.SerializeObject(data));
                        return Task.CompletedTask;
                    }
                    else
                    {
                        evnt.Response.Redirect("/Account/Login" + "?ReturnUrl=" + evnt.Request.Path);
                    }

                    return Task.CompletedTask;
                };
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

            });

            services.AddMvc()
            .AddMvcOptions(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddNToastNotifyToastr(new ToastrOptions()
            {
                ProgressBar = true,
                TimeOut = 1500,
                PositionClass = ToastPositions.TopRight
            })
             .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);




            services.AddScoped<UserServiceInterface, UserService>();
            services.AddScoped<UserRepositoryInterface, UserRepository>();

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
            services.AddScoped<CustomerTansactionRepositoryInterface, CustomerTransactionRepository>();
            services.AddScoped<CustomerTransactionServiceInterface, CustomerTransactionService>();
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
            app.UseCookiePolicy();
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
