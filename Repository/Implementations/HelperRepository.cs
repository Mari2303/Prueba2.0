using API.Entity.Context;
using API.Entity.Dtos;
using API.Entity.Models;
using API.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace API.Repository.Implementations
{
    /// <summary>
    /// Implementación concreta de la clase <see cref="AHelperRepository{T, D}"/> que utiliza reflexión y árboles de expresiones
    /// para validar dinámicamente si una entidad del tipo <typeparamref name="T"/> es referenciada por otras entidades activas en la base de datos.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad, que debe heredar de <see cref="BaseModel"/>.</typeparam>
    /// <typeparam name="D">El tipo DTO, que debe heredar de <see cref="BaseDTO"/>.</typeparam>
    public class HelperRepository<T, D> : IHelperRepository<T, D>
        where T : BaseModel
        where D : BaseDTO
    {
        private readonly OperativeContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="HelperRepository{T, D}"/> con el contexto de base de datos y el mapeador especificados.
        /// </summary>
        /// <param name="context">El contexto de base de datos Entity Framework utilizado para consultar relaciones entre entidades.</param>
        /// <param name="mapper">El mapeador de objetos utilizado para transformar modelos a DTOs.</param>
        public HelperRepository(OperativeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Valida si la entidad del tipo <typeparamref name="T"/> con el ID proporcionado está referenciada por registros activos
        /// en otras entidades dentro del contexto actual de la base de datos.
        /// </summary>
        /// <param name="id">El identificador de la entidad a validar.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona. El resultado de la tarea es <c>true</c> si no existen dependencias activas de clave foránea;
        /// de lo contrario, <c>false</c>.
        /// </returns>
        public  async Task<bool> ValidateEntityRelationships(int id)
        {
            var targetEntityName = typeof(T).Name;

            foreach (var entityType in _context.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;
                var properties = clrType.GetProperties();

                var navigationExists = properties.Any(p =>
                    p.PropertyType == typeof(T) ||
                    p.PropertyType.IsClass && p.PropertyType.Name == targetEntityName);

                if (!navigationExists)
                    continue;

                var fkProperty = properties.FirstOrDefault(p =>
                    p.Name == targetEntityName + "Id" && p.PropertyType == typeof(int));

                var stateProperty = properties.FirstOrDefault(p =>
                    p.Name == "State" && p.PropertyType == typeof(bool));

                if (fkProperty == null || stateProperty == null)
                    continue;

                var setMethod = typeof(DbContext).GetMethod(nameof(DbContext.Set), Type.EmptyTypes);
                var genericSetMethod = setMethod!.MakeGenericMethod(clrType);
                var dbSet = genericSetMethod.Invoke(_context, null);
                var queryable = dbSet as IQueryable;

                if (queryable == null)
                    continue;

                var parameter = Expression.Parameter(clrType, "x");
                var propertyFk = Expression.Property(parameter, fkProperty);
                var propertyState = Expression.Property(parameter, stateProperty);

                var constantId = Expression.Constant(id);
                var constantTrue = Expression.Constant(true);

                var fkEquals = Expression.Equal(propertyFk, constantId);
                var stateEquals = Expression.Equal(propertyState, constantTrue);

                var combinedCondition = Expression.AndAlso(fkEquals, stateEquals);
                var lambda = Expression.Lambda(combinedCondition, parameter);

                var whereMethod = typeof(Queryable)
                    .GetMethods()
                    .First(m => m.Name == "Where" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(clrType);

                var filteredQuery = whereMethod.Invoke(null, new object[] { queryable, lambda });

                var toListAsyncMethod = typeof(EntityFrameworkQueryableExtensions)
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .First(m => m.Name == "ToListAsync" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(clrType);

                var task = (Task)toListAsyncMethod.Invoke(null, new object[] { filteredQuery, CancellationToken.None })!;
                await task.ConfigureAwait(false);

                var resultProperty = task.GetType().GetProperty("Result");
                var list = resultProperty!.GetValue(task) as IEnumerable<object>;

                if (list!.Any())
                    return false;
            }
            return true;
        }
    }
}
