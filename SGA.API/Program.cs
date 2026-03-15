using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SGA.Application.Interfaces;
using SGA.Application.Servicios;
using SGA.Domain.Repository;
using SGA.Persistence.Contexts;
using SGA.Persistence.UnitOfWork;
using System.Text;

namespace SGA.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ─── Infraestructura ───
            builder.Services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // ─── Servicios de Aplicación ───
            builder.Services.AddScoped<IPersonaService, PersonaService>();
            builder.Services.AddScoped<IAutobusService, AutobusService>();
            builder.Services.AddScoped<IRutaService, RutaService>();
            builder.Services.AddScoped<IViajeService, ViajeService>();
            builder.Services.AddScoped<IAutorizacionService, AutorizacionService>();
            builder.Services.AddScoped<IAccesoService, AccesoService>();
            builder.Services.AddScoped<IIncidenciaService, IncidenciaService>();
            builder.Services.AddScoped<IRegistroUsoService, RegistroUsoService>();
            builder.Services.AddScoped<IConductorService, ConductorService>();

            // ─── Autenticación JWT ───
            var jwtKey = builder.Configuration["Jwt:Key"]!;
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                                                  Encoding.UTF8.GetBytes(jwtKey))
                };
            });

            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("AdminOnly", p => p.RequireRole("Administrador"));
                opt.AddPolicy("ConductorOnly", p => p.RequireRole("Conductor"));
                opt.AddPolicy("UsuarioFinal", p => p.RequireRole("Estudiante", "Empleado"));

                opt.AddPolicy("AdminOrConductor", p =>
                    p.RequireRole("Administrador", "Conductor"));
            });

            // ─── Controllers & Swagger ───
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SGA-ITLA API",
                    Version = "v1",
                    Description = "Sistema de Gestión de Autobuses del ITLA"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header. Ejemplo: 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id   = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // ─── CORS ───
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}