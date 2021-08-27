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
    public class GetNewInvoiceNumberQuery : IRequest<Result<InvoiceNumberDto>>
    {
        public long NewInvoiceNumber { get; set; }
    }

    public class GetNewInvoiceNumberQueryHandler : IRequestHandler<GetNewInvoiceNumberQuery, Result<InvoiceNumberDto>>
    {
        private readonly ISIMContext _context;
        private readonly IMapper _mapper;

        public GetNewInvoiceNumberQueryHandler(ISIMContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<InvoiceNumberDto>> Handle(GetNewInvoiceNumberQuery request, CancellationToken cancellationToken)
        {
            var maxNumber = await _context.Invoices
                .MaxAsync(x => x.Number,
                cancellationToken);

            maxNumber++;

            var numberDto = new NumberDto() { NewInvoiceNumber = maxNumber };

            return Result<InvoiceNumberDto>.SuccessFul(_mapper.Map<InvoiceNumberDto>(numberDto));
        }
    }
}