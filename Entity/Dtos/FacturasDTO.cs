using API.Entity.Dtos;

namespace Entity.Dtos
{
    public class FacturasDTO : BaseDTO
    {


        public string Cliente { get; set; }

        public DateTime Fecha { get; set; }

        public List<DetallesFacturasDTO> Detalles { get; set; }



    }
}
