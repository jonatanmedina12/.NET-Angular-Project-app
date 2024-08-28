using API.Extensiones;
using API.Middleware;
using Data;
using Data.Inicializador;
using Data.Interfaces;
using Data.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Agregar servicios de aplicación personalizados
builder.Services.AgregarServiciosAplicacion(builder.Configuration);

// Agregar servicios de identidad
builder.Services.AgregarServiciosIdentidad(builder.Configuration);
builder.Services.AddScoped<IdbInicializador,DbInicializador>();
// Configurar logging
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Build the application
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errores/{0}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var inicializador = services.GetRequiredService<IdbInicializador>();
        inicializador.Inicializar();
    }
    catch(Exception ex) {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "uN ERROR OCURIO AL EJECUTAR LA MIGRACION");
    }
}
app.MapControllers();

app.Run();