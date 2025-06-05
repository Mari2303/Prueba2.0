using API.Entity.Dtos;

namespace Entity.Dtos
{
    public class DetallesFacturasDTO : BaseDTO
    {
        public string Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }
    }
}
