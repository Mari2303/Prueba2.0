using API.Entity.Dtos;
using API.Entity.Models;
using API.Service.Interfaces;

namespace API.Service.Implementations
{
    /// <summary>
    /// Clase base abstracta para servicios auxiliares que provee un contrato para validar las relaciones de una entidad.
    /// Implementa la interfaz <see cref="IHelperService{T, D}"/>.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad, que debe heredar de <see cref="BaseModel"/>.</typeparam>
    /// <typeparam name="D">El tipo de Data Transfer Object (DTO), que debe heredar de <see cref="BaseDTO"/>.</typeparam>
    public abstract class AHelperService<T, D> : IHelperService<T, D>
        where T : BaseModel
        where D : BaseDTO
    {
        /// <summary>
        /// Cuando se implemente en una clase derivada, valida las relaciones asociadas con la entidad identificada por el ID especificado.
        /// </summary>
        /// <param name="id">El identificador único de la entidad.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona. El resultado indica si las relaciones de la entidad son válidas.
        /// </returns>
        public abstract Task<bool> ValidateEntityRelationships(int id);
    }
}




