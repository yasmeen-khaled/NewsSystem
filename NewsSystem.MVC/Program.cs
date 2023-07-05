using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsSystem.DAL;
using NewsSystem.MVC.RepoService;

namespace NewsSystem.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("SystemContextConnection") ?? throw new InvalidOperationException("Connection string 'SystemContextConnection' not found.");

            builder.Services.AddDbContext<SystemContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<SystemContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddHttpContextAccessor();

            #region httpService fro calling API

            builder.Services.AddHttpClient();

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}