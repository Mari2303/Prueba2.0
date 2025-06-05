using API.Entity.Dtos;
using API.Entity.Models;

namespace API.Service.Interfaces
{
    /// <summary>
    /// Define el contrato para una capa de servicio genérica que realiza operaciones CRUD básicas
    /// sobre entidades y sus correspondientes Objetos de Transferencia de Datos (DTOs).
    /// </summary>
    /// <typeparam name="T">El tipo de entidad, que debe heredar de <see cref="BaseModel"/>.</typeparam>
    /// <typeparam name="D">El tipo de DTO, que debe heredar de <see cref="BaseDTO"/>.</typeparam>
    public interface IBaseModelService<T, D>
        where T : BaseModel
        where D : BaseDTO
    {
        /// <summary>
        /// Recupera una entidad específica por su identificador único.
        /// </summary>
        /// <param name="id">El ID de la entidad a recuperar.</param>
        /// <returns>Una tarea que representa la operación asincrónica, que contiene la entidad si se encuentra.</returns>
        Task<T> GetById(int id);

        /// <summary>
        /// Persiste una nueva entidad en el almacén de datos subyacente.
        /// </summary>
        /// <param name="entity">La entidad que se va a guardar.</param>
        /// <returns>Una tarea que representa la operación asincrónica, que contiene la entidad guardada.</returns>
        Task<T> Save(T entity);

        /// <summary>
        /// Actualiza una entidad existente en el almacén de datos.
        /// </summary>
        /// <param name="entity">La entidad con los valores actualizados.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        Task Update(T entity);

        /// <summary>
        /// Elimina una entidad basada en su identificador único.
        /// </summary>
        /// <param name="id">El ID de la entidad a eliminar.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        Task Delete(int id);
    }
}
