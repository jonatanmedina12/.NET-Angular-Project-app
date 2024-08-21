using API.Errores;
using BLL.Servicios;
using BLL.Servicios.Interfaces;
using Data;
using Data.Interfaces;
using Data.Interfaces.IRepositorio;
using Data.Repositorio;
using Data.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Utilidades;

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
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ActionContext =>
                {
                    var errores = ActionContext.ModelState
                                                .Where(e => e.Value.Errors.Count() > 0)
                                                .SelectMany(x => x.Value.Errors)
                                                .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidacionErrorResponse
                    {
                        Errores = errores
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            services.AddScoped<IUnidadTrabajo, UnidadTrabajo>();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddScoped<IEspecialidadServicio,EspecialidadServicio>();


            return services;
        }
    }
}