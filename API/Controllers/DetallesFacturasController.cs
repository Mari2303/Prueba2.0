using API.Service.Interfaces;
using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace API.Controllers
{
    public class DetallesFacturasController : BaseModelController<DetallesFacturas, DetallesFacturasDTO>
    {
        private readonly IDetallesFacturasService _service;
        private readonly IMapper _mapper;

        public DetallesFacturasController(IBaseModelService<DetallesFacturas, DetallesFacturasDTO> baseService, IDetallesFacturasService service, IMapper mapper) : base(baseService, mapper)
        {
            _service = service;
            _mapper = mapper;
        }
    }
}
