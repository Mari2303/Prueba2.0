using API.Entity.Context;
using API.Repository.Implementations;
using API.Repository.Interfaces;
using API.Service.Implementations;
using API.Service.Interfaces;
using API.Service.Mappers;
using Entity.Dtos;
using Entity.Models;

using Microsoft.EntityFrameworkCore;

using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configurar el contexto de base de datos con sql serve
builder.Services.AddDbContext<OperativeContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfiles>());

// Configurar servicios para evitar referencias cíclicas en JSON
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Configurar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===== Inyección de dependencias directamente aquí =====

// Facturas
builder.Services.AddScoped<IFacturasRepository, FacturasRepository>();
builder.Services.AddScoped<IBaseModelService<Facturas, FacturasDTO>, FacturasService>();
builder.Services.AddScoped<IFacturasService, FacturasService>();

// DetallesFacturas
builder.Services.AddScoped<IDetallesFacturasRepository, DetallesFaturasRepository>();
builder.Services.AddScoped<IBaseModelService<DetallesFacturas, DetallesFacturasDTO>, DetallesFacturasService>();
builder.Services.AddScoped<IDetallesFacturasService, DetallesFacturasService>();

builder.Services.AddScoped<IHelperRepository<Facturas, FacturasDTO>, HelperRepository<Facturas, FacturasDTO>>();
builder.Services.AddScoped<IHelperRepository<DetallesFacturas, DetallesFacturasDTO>, HelperRepository<DetallesFacturas, DetallesFacturasDTO>>();


// =============================================

var app = builder.Build();

// Middleware para desarrollo: Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS
app.UseCors(policy =>
{
    policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

// Seguridad
app.UseAuthentication();
app.UseAuthorization();

// HTTPS y rutas
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
