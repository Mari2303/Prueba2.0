using API.Repository.Interfaces;
using API.Service.Interfaces;
using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace API.Service.Implementations
{
    public class DetallesFacturasService : BaseModelService<DetallesFacturas, DetallesFacturasDTO>, IDetallesFacturasService
    {
        private readonly IDetallesFacturasRepository _invoiceDetailRepository;
        private readonly IHelperService<DetallesFacturas, DetallesFacturasDTO> _helperService;
        private readonly IMapper _mapper;

        public DetallesFacturasService(IDetallesFacturasRepository invoiceDetailRepository, IHelperService<DetallesFacturas, DetallesFacturasDTO> helperService,  IMapper mapper) : base(invoiceDetailRepository, helperService, mapper)
        {
            _invoiceDetailRepository = invoiceDetailRepository;
            _mapper = mapper;
            _helperService = helperService;
        }
    }
}
