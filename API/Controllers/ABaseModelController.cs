using API.Entity.Dtos;
using API.Entity.Models;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    /// <summary>
    /// Controlador base abstracto que define endpoints genéricos RESTful para gestionar entidades y sus representaciones DTO.
    /// Esta clase está pensada para ser heredada por controladores específicos que manejen un tipo de modelo concreto.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad, que hereda de <see cref="BaseModel"/>.</typeparam>
    /// <typeparam name="D">El tipo DTO, que hereda de <see cref="BaseDTO"/>.</typeparam>
    public abstract class ABaseModelController<T, D> : ControllerBase
        where T : BaseModel
        where D : BaseDTO
    {

        /// <summary>
        /// Obtiene una entidad específica por su identificador único y la mapea a un DTO.
        /// </summary>
        /// <param name="id">El ID de la entidad a obtener.</param>
        /// <returns>La representación DTO de la entidad, envuelta en un <see cref="ActionResult"/>.</returns>
        public abstract Task<ActionResult<D>> GetById(int id);

        /// <summary>
        /// Crea una nueva entidad usando los datos del DTO proporcionado.
        /// </summary>
        /// <param name="dto">El objeto de transferencia de datos que contiene la información de la entidad a guardar.</param>
        /// <returns>La entidad creada como DTO, envuelta en un <see cref="ActionResult"/>.</returns>
        public abstract Task<ActionResult<D>> Save(D request);

        /// <summary>
        /// Actualiza una entidad existente usando los datos del DTO proporcionado.
        /// </summary>
        /// <param name="dto">El objeto de transferencia de datos que contiene la información actualizada de la entidad.</param>
        /// <returns>Un <see cref="ActionResult"/> que indica el resultado de la operación de actualización.</returns>
        public abstract Task<ActionResult<D>> Update(D request);

        /// <summary>
        /// Elimina una entidad basada en su identificador único.
        /// </summary>
        /// <param name="id">El ID de la entidad a eliminar.</param>
        /// <returns>Un <see cref="ActionResult"/> que indica el resultado de la operación de eliminación.</returns>
        public abstract Task<ActionResult> Delete(int id);
    }

}
