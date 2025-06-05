using API.Entity.Dtos;
using API.Entity.Models;

namespace API.Service.Interfaces
{
    /// <summary>
    /// Define un contrato para servicios auxiliares que operan sobre un modelo y su correspondiente Data Transfer Object (DTO).
    /// </summary>
    /// <typeparam name="T">El tipo de modelo, que debe heredar de <see cref="BaseModel"/>.</typeparam>
    /// <typeparam name="D">El tipo de DTO, que debe heredar de <see cref="BaseDTO"/>.</typeparam>
    public interface IHelperService<T, D>
        where T : BaseModel
        where D : BaseDTO
    {
        /// <summary>
        /// Valida las relaciones asociadas con una entidad dada identificada por su ID.
        /// </summary>
        /// <param name="id">El identificador único de la entidad cuyas relaciones se validarán.</param>
        /// <returns>
        /// Una tarea que representa la operación asincrónica. El resultado de la tarea contiene <c>true</c> si las relaciones son válidas; de lo contrario, <c>false</c>.
        /// </returns>
        Task<bool> ValidateEntityRelationships(int id);
    }
}

