namespace API.Entity.Dtos
{
    /// <summary>
    /// Objeto de transferencia de datos para la información de la entidad.
    /// </summary>
    public class BaseDTO
    {
        /// <summary>
        /// Identificador único de la entidad.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Indica si la entidad está actualmente activa en el sistema.
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// Fecha y hora en que la entidad fue creada.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Fecha y hora en que la entidad fue eliminada por última vez.
        /// </summary>
        public DateTime? DeletedAt { get; set; }
    }
}

