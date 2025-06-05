using Entity.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace API.Entity.Context
{
    /// <summary>
    /// Configura las entidades para la aplicación.
    /// Define las claves primarias.
    /// </summary>
    public class ApplicationEntityConfigurations :
        IEntityTypeConfiguration<Facturas>,
        IEntityTypeConfiguration<DetallesFacturas>
    {
        /// <summary>
        /// Configura la entidad Facturas.
        /// Define la clave primaria para la entidad Facturas.
        /// </summary>
        /// <param name="builder">El constructor usado para configurar la entidad.</param>
        public void Configure(EntityTypeBuilder<Facturas> builder)
        {
            builder.HasKey(s => s.Id); // Clave primaria
        }

        /// <summary>
        /// Configura la entidad DetallesFacturas.
        /// Define la clave primaria y la relación con Facturas.
        /// </summary>
        /// <param name="builder">El constructor usado para configurar la entidad.</param>
        public void Configure(EntityTypeBuilder<DetallesFacturas> builder)
        {
            builder.HasKey(s => s.Id); // Clave primaria

            // Definición corregida de relaciones
            builder.HasOne(d => d.Facturas)
                .WithMany(c => c.Detalles)
                .HasForeignKey(d => d.FacturaId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
