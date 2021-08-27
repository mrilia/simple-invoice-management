using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIM.Application.Handlers.Invoices.Dto;
using SIM.Application.Interfaces;
using SIM.Application.Messages;
using SIM.Application.Result;

namespace SIM.Application.Handlers.Invoices.Queries
{
    public class GetInvoiceQuery : IRequest<Result<InvoiceDto>>
    {
        public long Number { get; set; }
    }

    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, Result<InvoiceDto>>
    {
        private readonly ISIMContext _context;
        private readonly IMapper _mapper;

        public GetInvoiceQueryHandler(ISIMContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<InvoiceDto>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _context.Invoices
                .Include(e => e.InvoiceRows)
                .FirstAsync(x => x.Number == request.Number,
                cancellationToken);

            if (invoice is null)
                return Result<InvoiceDto>.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.InvoiceNotFound)));

            return Result<InvoiceDto>.SuccessFul(_mapper.Map<InvoiceDto>(invoice));
        }
    }
}