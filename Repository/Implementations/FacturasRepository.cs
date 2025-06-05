using API.Entity.Context;
using API.Repository.Interfaces;
using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using Microsoft.Extensions.Configuration;

namespace API.Repository.Implementations
{
    public class FacturasRepository : BaseModelRepository<Facturas, FacturasDTO>, IFacturasRepository
    {
        private readonly OperativeContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public FacturasRepository(OperativeContext context, IMapper mapper, IConfiguration configuration) : base(context, mapper, configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
    }
}
