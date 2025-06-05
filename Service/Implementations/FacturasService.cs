using API.Repository.Interfaces;
using API.Service.Interfaces;
using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace API.Service.Implementations
{
    public class FacturasService : BaseModelService<Facturas, FacturasDTO>, IFacturasService
    {
        private readonly IFacturasRepository _invoiceRepository;
        private readonly IHelperService<Facturas, FacturasDTO> _helperService;
        private readonly IMapper _mapper;

        public FacturasService(IFacturasRepository invoiceRepository, IHelperService<Facturas, FacturasDTO> helperService, IMapper mapper) : base(invoiceRepository, helperService, mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
            _helperService = helperService;
        }
    }
}
