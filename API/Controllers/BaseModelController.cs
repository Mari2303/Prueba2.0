using API.Entity.Dtos;
using API.Entity.Models;
using API.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace API.Controllers
{
    /// <summary>
    /// Controlador base genérico para manejar operaciones CRUD utilizando una capa de servicio y AutoMapper.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad, que hereda de <see cref="BaseModel"/>.</typeparam>
    /// <typeparam name="D">El tipo de objeto de transferencia de datos (DTO), que hereda de <see cref="BaseDTO"/>.</typeparam>

    [ApiController]
    [Route("api/[controller]")]
    public class BaseModelController<T, D> : ABaseModelController<T, D>
        where T : BaseModel
        where D : BaseDTO
    {
        private readonly IBaseModelService<T, D> _service;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="BaseModelController{T, D}"/>.
        /// </summary>
        /// <param name="service">La instancia del servicio utilizada para las operaciones de lógica de negocio.</param>
        /// <param name="mapper">La instancia de AutoMapper usada para mapear entre entidades y DTOs.</param>

        public BaseModelController(IBaseModelService<T, D> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }



        /// <summary>
        /// Obtiene un registro individual por su ID.
        /// </summary>
        /// <param name="id">El ID del registro que se desea obtener.</param>
        /// <returns>Un <see cref="ActionResult"/> que contiene el DTO o un mensaje de error.</returns>

        [AllowAnonymous]
        [HttpGet("{id}")]
        public override async Task<ActionResult<D>> GetById(int id)
        {
            try
            {
                T data = await _service.GetById(id);

                if (data == null)
                {
                    // Puedes devolver un objeto anónimo o un DTO de error, pero aquí se usa NotFound sin ApiResponse
                    return NotFound(new { success = false, message = "Record not found" });
                }

                var dto = _mapper.Map<D>(data);
                // Devuelve el DTO directamente o envuélvelo en un objeto de respuesta si lo prefieres
                return Ok(dto);
            }
            catch (Exception ex)
            {
                // Devuelve un error 500 con un mensaje personalizado
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = ex.Message });
            }
        }


        /// <summary>
        /// Almacena un nuevo registro basado en el DTO proporcionado.
        /// </summary>
        /// <param name="dto">El DTO que representa el nuevo registro.</param>
        /// <returns>Un <see cref="ActionResult"/> con el registro almacenado o un mensaje de error.</returns>

        [AllowAnonymous]
        [HttpPost]
        public override async Task<ActionResult<D>> Save([FromQuery] D request)
        {
            try
            {
                T saved = await _service.Save(_mapper.Map<T>(request));

                // Create a response object manually since ApiResponse expects specific types
                var response = new
                {
                    Content = request,
                    IsSuccessful = true,
                    Message = "Record stored successfully"
                };

                return new CreatedAtRouteResult(new { id = saved.Id }, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    Content = (IEnumerable<D>?)null,
                    IsSuccessful = false,
                    Message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Elimina un registro por su ID.
        /// </summary>
        /// <param name="id">El ID del registro que se desea eliminar.</param>
        /// <returns>Un <see cref="ActionResult"/> que indica el resultado de la operación.</returns>

        [AllowAnonymous]
        [HttpPut]
        public override async Task<ActionResult<D>> Update([FromQuery] D request)
        {
            try
            {
                await _service.Update(_mapper.Map<T>(request));

                // Create a response object manually since ApiResponse expects specific types
                var response = new
                {
                    Content = request,
                    IsSuccessful = true,
                    Message = "Record updated successfully"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    Content = (IEnumerable<D>?)null,
                    IsSuccessful = false,
                    Message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }


        /// <summary>
        /// Elimina un registro por su ID.
        /// </summary>
        /// <param name="id">El ID del registro que se desea eliminar.</param>
        /// <returns>Un <see cref="ActionResult"/> que indica el resultado de la operación.</returns>

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public override async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);

                var successResponse = new
                {
                    Content = (D?)null,
                    IsSuccessful = true,
                    Message = "Record successfully deleted"
                };
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    Content = (D?)null,
                    IsSuccessful = false,
                    Message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

    }
}
