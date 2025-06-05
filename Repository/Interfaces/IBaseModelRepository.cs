using API.Entity.Dtos;
using API.Entity.Models;

namespace API.Repository.Interfaces
{
    /// <summary>
    /// Interfaz para operaciones relacionadas con entidades base.
    /// Esta interfaz define métodos para recuperar, agregar y actualizar entidades base en el repositorio.
    /// </summary>
    public interface IBaseModelRepository<T, D> where T : BaseModel where D : BaseDTO
    {
        /// <summary>
        /// Recupera una entidad por su ID.
        /// </summary>
        /// <param name="id">El ID de la entidad.</param>
        /// <returns>
        /// Una tarea que representa la operación asincrónica, con un resultado de tipo <see cref="T"/>.
        /// </returns>
        Task<T> GetById(int id);

        /// <summary>
        /// Agrega una nueva entidad al repositorio.
        /// </summary>
        /// <param name="entity">La entidad que se va a agregar.</param>
        /// <returns>
        /// Una tarea que representa la operación asincrónica, con un resultado de tipo <see cref="T"/>.
        /// </returns>
        Task<T> Save(T entity);

        /// <summary>
        /// Actualiza una entidad existente en el repositorio.
        /// </summary>
        /// <param name="entity">La entidad con los datos actualizados.</param>
        /// <returns>
        /// Una tarea que representa la operación asincrónica.
        /// </returns>
        Task Update(T entity);
    }
}

