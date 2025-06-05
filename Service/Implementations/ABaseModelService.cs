using API.Entity.Dtos;
using API.Entity.Models;
using API.Service.Interfaces;

namespace API.Service.Implementations
{
    /// <summary>
    /// Clase base abstracta que define la estructura para una capa de servicios que maneja operaciones CRUD genéricas.
    /// Esta clase debe ser heredada y sus métodos deben ser implementados por una clase de servicio concreta.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad, que debe heredar de <see cref="BaseModel"/>.</typeparam>
    /// <typeparam name="D">El tipo DTO, que debe heredar de <see cref="BaseDTO"/>.</typeparam>
    public abstract class ABaseModelService<T, D> : IBaseModelService<T, D>
        where T : BaseModel
        where D : BaseDTO
    {
        /// <summary>
        /// Elimina una entidad del almacén de datos según su identificador único.
        /// </summary>
        /// <param name="id">El ID de la entidad a eliminar.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public abstract Task Delete(int id);

        /// <summary>
        /// Recupera una sola entidad basada en su identificador único.
        /// </summary>
        /// <param name="id">El ID de la entidad a recuperar.</param>
        /// <returns>Una tarea que representa la operación asincrónica, que contiene la entidad si se encuentra.</returns>
        public abstract Task<T> GetById(int id);

        /// <summary>
        /// Guarda una nueva entidad en el almacén de datos.
        /// </summary>
        /// <param name="entity">La entidad a guardar.</param>
        /// <returns>Una tarea que representa la operación asincrónica, que contiene la entidad guardada.</returns>
        public abstract Task<T> Save(T entity);

        /// <summary>
        /// Actualiza una entidad existente en el almacén de datos.
        /// </summary>
        /// <param name="entity">La entidad a actualizar.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public abstract Task Update(T entity);
    }
}
