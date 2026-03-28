using SGA.Persistence.Contexts;
using SGA.Domain.Repository;
using SGA.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace SGA.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            


            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

            
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.MigrationsAssembly("SGA.Infrastructure")));

            
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



            var app = builder.Build();

     
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.MapRazorPages();

  
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

