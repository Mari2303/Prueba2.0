using API.Entity.Dtos;
using API.Entity.Models;
using API.Repository.Interfaces;

namespace API.Repository.Implementations
{
    /// <summary>
    /// Clase base abstracta para repositorios auxiliares que provee un contrato para validar las relaciones de una entidad.
    /// Implementa la interfaz <see cref="IHelperRepository{T, D}"/>.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad, que hereda de <see cref="BaseModel"/>.</typeparam>
    /// <typeparam name="D">El tipo de Data Transfer Object (DTO), que hereda de <see cref="BaseDTO"/>.</typeparam>
    public abstract class AHelperRepository<T, D> : IHelperRepository<T, D>
        where T : BaseModel
        where D : BaseDTO
    {
        /// <summary>
        /// Cuando se implemente en una clase derivada, valida las relaciones de la entidad identificada por el ID especificado.
        /// </summary>
        /// <param name="id">El identificador único de la entidad a validar.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona. El resultado de la tarea contiene 
        /// <c>true</c> si las relaciones son válidas; de lo contrario, <c>false</c>.
        /// </returns>
        public abstract Task<bool> ValidateEntityRelationships(int id);
    }
}

