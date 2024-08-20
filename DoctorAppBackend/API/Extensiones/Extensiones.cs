using Data;
using Data.Interfaces;
using Data.Servicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace API.Extensiones
{
    public static class Extensiones
    {
        public static IServiceCollection AgregarServiciosAplicacion(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Ingresar Bearer [espacio] token \r\n\r\n" +
                                  "Ejemplo: Bearer ejoyá2029283883",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("API")));

            services.AddCors();
            services.AddScoped<ITokenServicio, TokenServicio>();

            return services;
        }
    }
}