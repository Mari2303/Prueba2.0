using API.Entity.Dtos;
using API.Entity.Models;

namespace API.Repository.Interfaces
{
    /// <summary>
    /// Define un contrato para las operaciones del repositorio relacionadas con la validación de relaciones de un tipo específico de entidad.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad, que hereda de <see cref="BaseModel"/>.</typeparam>
    /// <typeparam name="D">El tipo de DTO (Data Transfer Object), que hereda de <see cref="BaseDTO"/>.</typeparam>
    public interface IHelperRepository<T, D>
        where T : BaseModel
        where D : BaseDTO
    {
        /// <summary>
        /// Valida las relaciones asociadas con la entidad identificada por el ID especificado.
        /// </summary>
        /// <param name="id">El identificador único de la entidad a validar.</param>
        /// <returns>
        /// Una tarea que representa la operación asincrónica. El resultado de la tarea contiene 
        /// <c>true</c> si las relaciones son válidas; de lo contrario, <c>false</c>.
        /// </returns>
        Task<bool> ValidateEntityRelationships(int id);
    }
}


