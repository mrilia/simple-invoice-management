using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIM.Application.Interfaces;
using SIM.Application.Messages;
using SIM.Application.Result;
using SIM.Domain.Models.Invoice;

namespace SIM.Application.Handlers.Invoices.Command.Update
{
    public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, Result.Result>
    {
        private readonly ISIMContext _context;
        private readonly IMapper _mapper;

        public UpdateInvoiceCommandHandler(ISIMContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result.Result> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await GetInvoiceAsync(request, cancellationToken);

            if (invoice is null)
                return Result.Result.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.InvoiceNotFound)));

            _mapper.Map(request, invoice);

            await _context.SaveAsync(cancellationToken);

            return Result.Result.SuccessFul();
        }

        private async Task<Invoice> GetInvoiceAsync(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            return await _context.Invoices
                .Include(e => e.InvoiceRows)
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
        
    }
}