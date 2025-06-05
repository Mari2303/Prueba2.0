using API.Entity.Context;
using API.Entity.Dtos;
using API.Entity.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Repository.Implementations
{
    /// <summary>
    /// Implementación concreta del repositorio abstracto para realizar operaciones genéricas CRUD.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad, que debe heredar de <see cref="BaseModel"/>.</typeparam>
    /// <typeparam name="D">El tipo de objeto de transferencia de datos (DTO), que debe heredar de <see cref="BaseDTO"/>.</typeparam>
    public class BaseModelRepository<T, D> : ABaseModelRepository<T, D>
        where T : BaseModel
        where D : BaseDTO
    {
        private readonly OperativeContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="BaseModelRepository{T, D}"/>.
        /// </summary>
        /// <param name="context">El contexto de base de datos para acceder a los conjuntos de entidades.</param>
        /// <param name="mapper">La instancia de AutoMapper utilizada para mapear entre entidades y DTOs.</param>
        /// <param name="configuration">Instancia para acceder a la configuración de la aplicación.</param>
        public BaseModelRepository(OperativeContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        /// <summary>
        /// Recupera una entidad por su identificador único.
        /// </summary>
        /// <param name="id">El ID de la entidad que se desea recuperar.</param>
        /// <returns>
        /// Una tarea que representa la operación asincrónica, conteniendo la entidad de tipo <typeparamref name="T"/> si se encuentra; en caso contrario, null.
        /// </returns>
        public override async Task<T> GetById(int id)
        {
            try
            {
                return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al recuperar los datos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Guarda una nueva entidad en la base de datos.
        /// </summary>
        /// <param name="entity">La entidad que se desea guardar.</param>
        /// <returns>
        /// Una tarea que representa la operación asincrónica, conteniendo la entidad guardada.
        /// </returns>
        public override async Task<T> Save(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar los datos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza una entidad existente en la base de datos.
        /// </summary>
        /// <param name="entity">La entidad que se desea actualizar.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public override async Task Update(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _context.Entry(entity).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar los datos: {ex.Message}");
                throw;
            }
        }
    }
}

