using API.Entity.Dtos;
using API.Entity.Models;
using API.Repository.Interfaces;
using API.Service.Interfaces;
using AutoMapper;

namespace API.Service.Implementations;
/// <summary>
 /// Implementación concreta del servicio auxiliar abstracto que maneja operaciones sobre entidades
 /// y delega la lógica de validación a la capa de repositorio.
 /// </summary>
 /// <typeparam name="T">El tipo de entidad, que hereda de <see cref="BaseModel"/>.</typeparam>
 /// <typeparam name="D">El tipo de objeto de transferencia de datos (DTO), que hereda de <see cref="BaseDTO"/>.</typeparam>
    public class HelperService<T, D> : IHelperService<T, D>
        where T : BaseModel
        where D : BaseDTO
    {
        private readonly IHelperRepository<T, D> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="HelperService{T, D}"/>.
        /// </summary>
        /// <param name="repository">La instancia del repositorio responsable del acceso a datos y validación de relaciones.</param>
        /// <param name="mapper">La instancia del mapeador usada para convertir entre entidades y DTOs.</param>
        public HelperService(IHelperRepository<T, D> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Valida las relaciones de la entidad especificada delegando la validación al repositorio.
        /// </summary>
        /// <param name="id">El identificador único de la entidad a validar.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona. El resultado de la tarea es 
        /// <c>true</c> si las relaciones son válidas; de lo contrario, <c>false</c>.
        /// </returns>
        public  async Task<bool> ValidateEntityRelationships(int id)
        {
            return await _repository.ValidateEntityRelationships(id);
        }
    }


