using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SIM.Application.Handlers.Invoices.Dto;
using SIM.Application.Interfaces;
using SIM.Application.Result;
using SIM.Domain.Models.Invoice;

namespace SIM.Application.Handlers.Invoices.Command.AddNew
{
    public class AddNewInvoiceCommandHandler : IRequestHandler<AddNewInvoiceCommand, Result<InvoiceDto>>
    {
        private readonly ISIMContext _context;
        private readonly IMapper _mapper;

        public AddNewInvoiceCommandHandler(ISIMContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<InvoiceDto>> Handle(AddNewInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoiceToAdd = _mapper.Map<Invoice>(request);

            await _context.Invoices.AddAsync(invoiceToAdd, cancellationToken);
            await _context.SaveAsync(cancellationToken);

            return Result<InvoiceDto>.SuccessFul(_mapper.Map<InvoiceDto>(invoiceToAdd));
        }
    }
}