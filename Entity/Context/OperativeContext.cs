using Dapper;
using Entity.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace API.Entity.Context
{
    /// <summary>
    /// Contexto de base de datos
    /// </summary>
    public class OperativeContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperativeContext"/>.
        /// </summary>
        /// <param name="options">Las opciones para el contexto.</param>
        /// <param name="configuration">El objeto de configuración.</param>
        public OperativeContext(DbContextOptions<OperativeContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Configura el modelo para el contexto de la base de datos.
        /// </summary>
        /// <param name="modelBuilder">El constructor del modelo para el contexto.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("parameter");

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            InitialData.Data(modelBuilder);

            // Aplica configuraciones para todas las entidades con una clase de configuración única.
            var configuration = new ApplicationEntityConfigurations();
            modelBuilder.ApplyConfiguration<Facturas>(configuration);
            modelBuilder.ApplyConfiguration<DetallesFacturas>(configuration);

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Configura el contexto para permitir el registro de datos sensibles, útil durante la depuración para ver valores de parámetros en los logs.
        /// Esto debe estar deshabilitado en entornos de producción para evitar riesgos de seguridad.
        /// </summary>
        /// <param name="optionsBuilder">El constructor de opciones.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        /// <summary>
        /// Sobrescribe el método `SaveChanges` para incluir lógica de auditoría antes de guardar los cambios en la base de datos.
        /// </summary>
        public override int SaveChanges()
        {
            EnsureAudit();
            return base.SaveChanges();
        }

        /// <summary>
        /// Sobrescribe el método `SaveChangesAsync` para incluir lógica de auditoría antes de guardar los cambios de forma asincrónica.
        /// </summary>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            EnsureAudit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Asegura la auditoría detectando cambios en las entidades antes de realizar operaciones en la base de datos.
        /// </summary>
        private void EnsureAudit()
        {
            ChangeTracker.DetectChanges();
        }

        /// <summary>
        /// Ejecuta una consulta SQL usando Dapper y devuelve una colección de objetos del tipo especificado.
        /// </summary>
        public async Task<IEnumerable<T>> QueryAsync<T>(string text, object parameters = null!, int? timeout = null, CommandType? type = null)
        {
            using var command = new DapperEFCoreCommand(this, text, parameters, timeout, type, CancellationToken.None);
            var connection = Database.GetDbConnection();
            return await connection.QueryAsync<T>(command.Definition);
        }

        /// <summary>
        /// Ejecuta una consulta SQL usando Dapper y devuelve el primer objeto del tipo especificado, o un valor por defecto si no se encuentra.
        /// </summary>
        public async Task<T> QueryFirstOrDefaultAsync<T>(string text, object parameters = null!, int? timeout = null, CommandType? type = null)
        {
            using var command = new DapperEFCoreCommand(this, text, parameters, timeout, type, CancellationToken.None);
            var connection = Database.GetDbConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(command.Definition);
        }

        /// <summary>
        /// Representa un comando SQL para ser ejecutado usando Dapper y Entity Framework Core, encapsulando detalles como transacción, tiempo de espera y token de cancelación.
        /// </summary>
        public readonly struct DapperEFCoreCommand : IDisposable
        {
            /// <summary>
            /// Inicializa el comando con los detalles proporcionados.
            /// </summary>
            /// <param name="context">El contexto de base de datos.</param>
            /// <param name="text">El texto de la consulta SQL.</param>
            /// <param name="parameters">Los parámetros para la consulta.</param>
            /// <param name="timeout">El tiempo de espera del comando.</param>
            /// <param name="type">El tipo de comando SQL.</param>
            /// <param name="ct">El token de cancelación.</param>
            public DapperEFCoreCommand(DbContext context, string text, object parameters, int? timeout, CommandType? type, CancellationToken ct)
            {
                var transaction = context.Database.CurrentTransaction?.GetDbTransaction();
                var commandType = type ?? CommandType.Text;
                var commandTimeout = timeout ?? context.Database.GetCommandTimeout() ?? 30;

                Definition = new CommandDefinition(
                    text,
                    parameters,
                    transaction,
                    commandTimeout,
                    commandType,
                    cancellationToken: ct
                );
            }

            public CommandDefinition Definition { get; }

            /// <summary>
            /// Implementación vacía de Dispose, ya que no se usan recursos no administrados en este comando.
            /// </summary>
            public void Dispose()
            {
                // No se requieren acciones adicionales para liberar recursos.
            }
        }

        // Definición de entidades como propiedades DbSet para la interacción con tablas de la base de datos.
        public DbSet<Facturas> Invoices { get; set; }
        public DbSet<DetallesFacturas> InvoiceDetails { get; set; }
    }
}
