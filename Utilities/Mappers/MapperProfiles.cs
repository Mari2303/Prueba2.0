using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace API.Service.Mappers
{
    //// <summary>
    /// Define los perfiles de AutoMapper para mapear entre DTOs, modelos de solicitud y modelos de entidad.
    /// Esta clase configura los mapeos bidireccionales utilizados en toda la aplicación.
    /// </summary>
    public class MapperProfiles : Profile
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MapperProfiles"/>
        /// y configura todos los mapeos necesarios entre objetos.
        /// </summary>
        public MapperProfiles() : base()
        {
            /// <summary>
            /// Mapea entre <see cref="FacturasDTO"/> y <see cref="Facturas"/> en ambas direcciones.
            /// </summary>
            CreateMap<FacturasDTO, Facturas>().ReverseMap();

            /// <summary>
            /// Mapea entre <see cref="DetallesFacturasDTO"/> y <see cref="Facturas"/> en ambas direcciones.
            /// </summary>
            CreateMap<DetallesFacturasDTO, Facturas>().ReverseMap();
        }
    }

}
