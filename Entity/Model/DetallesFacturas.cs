using API.Entity.Models;

namespace Entity.Models
{
    public class DetallesFacturas : BaseModel
    {
        public int FacturaId { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }


        public Facturas Facturas { get; set; }
    }
}
