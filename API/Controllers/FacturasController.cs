using API.Service.Interfaces;
using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace API.Controllers
{
    public class FacturasController : BaseModelController<Facturas, FacturasDTO>
    {
        private readonly IFacturasService _service;
        private readonly IMapper _mapper;

        public FacturasController(IBaseModelService<Facturas, FacturasDTO> baseService, IFacturasService service, IMapper mapper) : base(baseService, mapper)
        {
            _service = service;
            _mapper = mapper;
        }
    }
}
