using API.Entity.Dtos;
using API.Entity.Models;
using API.Repository.Interfaces;
using API.Service.Interfaces;
using AutoMapper;

namespace API.Service.Implementations
{
    /// <summary>
    /// Implementación concreta de la capa de servicios genérica para manejar operaciones CRUD estándar.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad, que hereda de <see cref="BaseModel"/>.</typeparam>
    /// <typeparam name="D">El tipo DTO (objeto de transferencia de datos), que hereda de <see cref="BaseDTO"/>.</typeparam>
    public class BaseModelService<T, D> : ABaseModelService<T, D>
        where T : BaseModel
        where D : BaseDTO
    {
        private readonly IBaseModelRepository<T, D> _repository;
        private readonly IHelperService<T, D> _helperService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="BaseModelService{T, D}"/>.
        /// </summary>
        /// <param name="repository">Instancia del repositorio utilizada para la persistencia de datos.</param>
        /// <param name="helperService">Servicio auxiliar utilizado para validar relaciones.</param>
        /// <param name="mapper">Instancia de AutoMapper utilizada para mapear entre entidades y DTOs.</param>
        public BaseModelService(IBaseModelRepository<T, D> repository, IHelperService<T, D> helperService, IMapper mapper)
        {
            _repository = repository;
            _helperService = helperService;
            _mapper = mapper;
        }

        /// <summary>
        /// Marca una entidad como eliminada estableciendo la marca de tiempo DeletedAt y desactivando su estado.
        /// </summary>
        /// <param name="id">El identificador único de la entidad a eliminar lógicamente.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        /// <exception cref="Exception">Se lanza si se encuentran relaciones asociadas que impiden la eliminación.</exception>
        public override async Task Delete(int id)
        {
            bool validateRelationships = await _helperService.ValidateEntityRelationships(id);
            if (validateRelationships)
            {
                T entity = await GetById(id);

                entity.DeletedAt = DateTime.UtcNow.AddHours(-5);
                entity.State = false;
                await _repository.Update(entity);
            }
            else
            {
                throw new Exception("Se encontraron entidades relacionadas, no se puede eliminar la entidad.");
            }
        }

        /// <summary>
        /// Recupera una entidad por su identificador único.
        /// </summary>
        /// <param name="id">El ID de la entidad a recuperar.</param>
        /// <returns>Una tarea que representa la operación asincrónica, que contiene la entidad si se encuentra.</returns>
        public override async Task<T> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        /// <summary>
        /// Guarda una nueva entidad en el repositorio, inicializando las propiedades CreatedAt y State.
        /// </summary>
        /// <param name="entity">La entidad a guardar.</param>
        /// <returns>Una tarea que representa la operación asincrónica, que contiene la entidad guardada.</returns>
        public override async Task<T> Save(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow.AddHours(-5);
            entity.State = true;
            return await _repository.Save(entity);
        }

        /// <summary>
        /// Actualiza una entidad existente en el repositorio, estableciendo la marca de tiempo UpdatedAt.
        /// </summary>
        /// <param name="entityUpdated">La entidad que se va a actualizar.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public override async Task Update(T entityUpdated)
        {
            T entity = await GetById(entityUpdated.Id);
            entityUpdated.CreatedAt = entity.CreatedAt;
            await _repository.Update(entityUpdated);
        }
    }
}

