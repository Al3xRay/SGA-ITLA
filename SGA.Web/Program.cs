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

            // ========== AGREGAR SERVICIOS ==========

            // Agregar servicios de Razor Pages
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

            // ========== CONFIGURAR ENTITY FRAMEWORK CORE ==========
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.MigrationsAssembly("SGA.Infrastructure")));

            // ========== REGISTRAR UNIT OF WORK ==========
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // ========== AGREGAR SERVICIOS DE APLICACIÓN (cuando se creen) ==========
            // Ejemplo:
            // builder.Services.AddScoped<IEstudianteService, EstudianteService>();

            var app = builder.Build();

            // ========== CONFIGURAR PIPELINE HTTP ==========
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Mapear Razor Pages
            app.MapRazorPages();

            // Mapear controladores
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

