using API.Entity.Dtos;
using API.Entity.Models;
using API.Repository.Interfaces;

namespace API.Repository.Implementations
{
    /// <summary>
    /// Clase abstracta base de repositorio que define operaciones genéricas para las capas de acceso a datos.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad, que debe heredar de <see cref="BaseModel"/>.</typeparam>
    /// <typeparam name="D">El tipo de objeto de transferencia de datos (DTO), que debe heredar de <see cref="BaseDTO"/>.</typeparam>
    public abstract class ABaseModelRepository<T, D> : IBaseModelRepository<T, D>
        where T : BaseModel
        where D : BaseDTO
    {
        /// <summary>
        /// Recupera una única entidad por su identificador único.
        /// </summary>
        /// <param name="id">El identificador único de la entidad.</param>
        /// <returns>Una tarea que representa la operación asincrónica, conteniendo la entidad de tipo <typeparamref name="T"/>.</returns>
        public abstract Task<T> GetById(int id);

        /// <summary>
        /// Guarda una nueva entidad en el almacén de datos.
        /// </summary>
        /// <param name="entity">La entidad de tipo <typeparamref name="T"/> que se va a guardar.</param>
        /// <returns>Una tarea que representa la operación asincrónica, conteniendo la entidad guardada.</returns>
        public abstract Task<T> Save(T entity);

        /// <summary>
        /// Actualiza una entidad existente en el almacén de datos.
        /// </summary>
        /// <param name="entity">La entidad de tipo <typeparamref name="T"/> que se va a actualizar.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public abstract Task Update(T entity);
    }
}

