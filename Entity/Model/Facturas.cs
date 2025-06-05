using API.Entity.Models;

namespace Entity.Models
{
    public class Facturas : BaseModel
    {
        public string Cliente { get; set; }
        public DateTime Fecha { get; set; }


        public ICollection<DetallesFacturas> Detalles { get; set; }
    }
}
