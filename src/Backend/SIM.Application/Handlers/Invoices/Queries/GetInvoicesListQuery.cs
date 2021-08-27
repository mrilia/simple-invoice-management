using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SIM.Application.Handlers.Invoices.Dto;
using SIM.Application.Interfaces;
using SIM.Domain.Models.Invoice;

namespace SIM.Application.Handlers.Invoices.Queries
{
    public class GetInvoicesListQuery : IRequest<List<InvoiceDto>>
    {
        public string Query { get; set; }
    }


    public class GetInvoiceListQueryHandler : IRequestHandler<GetInvoicesListQuery, List<InvoiceDto>>
    {
        private readonly ISIMContext _context;
        private readonly IMapper _mapper;

        public GetInvoiceListQueryHandler(ISIMContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<InvoiceDto>> Handle(GetInvoicesListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Invoice> invoices = _context.Invoices;

            if (!string.IsNullOrWhiteSpace(request.Query))
            {
                invoices = invoices.Where(x => x.BuyerName.Contains(request.Query))
                    .Include(e=>e.InvoiceRows);
            }

            return _mapper.Map<List<InvoiceDto>>(await invoices.Include(o=>o.InvoiceRows).ToListAsync(cancellationToken));
        }
    }
}