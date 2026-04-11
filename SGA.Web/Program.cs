using System.Net.Http.Headers;
using SGA.Web.Filters;
using SGA.Web.Services.Implementations;
using SGA.Web.Services.Interfaces;

namespace SGA.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AuthFilter>();
                options.Filters.Add<AdminOnlyFilter>();
            });
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.Name = ".SGA.Session";
            });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient("SgaApi", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            });
            builder.Services.AddScoped<IAuthApiService, AuthApiService>();
            builder.Services.AddScoped<IPersonaApiService, PersonaApiService>();
            builder.Services.AddScoped<IAutobusApiService, AutobusApiService>();
            builder.Services.AddScoped<IRutaApiService, RutaApiService>();
            builder.Services.AddScoped<IConductorApiService, ConductorApiService>();
            builder.Services.AddScoped<IViajeApiService, ViajeApiService>();
            builder.Services.AddScoped<IAutorizacionApiService, AutorizacionApiService>();
            builder.Services.AddScoped<IIncidenciaApiService, IncidenciaApiService>();
            builder.Services.AddScoped<IRegistroUsoApiService, RegistroUsoApiService>();
            builder.Services.AddScoped<IBilleteraApiService, BilleteraApiService>();
            builder.Services.AddScoped<IFinanzaApiService, FinanzaApiService>();
            builder.Services.AddScoped<IAuditoriaApiService, AuditoriaApiService>();
            builder.Services.AddScoped<IParadaApiService, ParadaApiService>();

            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
