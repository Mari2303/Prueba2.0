
/*
using API.Repository.Implementations;
using API.Repository.Interfaces;
using API.Service.Implementations;
using API.Service.Interfaces;
using Entity.Dtos;
using Entity.Models;

namespace API
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(IServiceCollection services)
        {
            // Dependency injection for invoice
            services.AddScoped<IFacturasRepository, FacturasRepository>();
            services.AddScoped<IBaseModelService<Facturas, FacturasDTO>, FacturasService>();
            services.AddScoped<IFacturasService, FacturasService>();

            // Dependency injection for invoiceDetail
            services.AddScoped<IDetallesFacturasRepository, DetallesFaturasRepository>();
            services.AddScoped<IBaseModelService<DetallesFacturas, DetallesFacturasDTO>, DetallesFacturasService>();
            services.AddScoped<IDetallesFacturasService, DetallesFacturasService>();

            // Dependency injection for helpers
            services.AddScoped<IHelperService<Facturas, FacturasDTO>, HelperService<Facturas, FacturasDTO>>();
            services.AddScoped<IHelperRepository<Facturas, FacturasDTO>, HelperRepository<Facturas, FacturasDTO>>();

            services.AddScoped<IHelperService<DetallesFacturas, DetallesFacturasDTO>, HelperService<DetallesFacturas, DetallesFacturasDTO>>();
            services.AddScoped<IHelperRepository<DetallesFacturas, DetallesFacturasDTO>, HelperRepository<DetallesFacturas, DetallesFacturasDTO>>();
        }
    }
}
*/