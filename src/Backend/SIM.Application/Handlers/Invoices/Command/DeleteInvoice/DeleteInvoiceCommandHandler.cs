using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIM.Application.Interfaces;
using SIM.Application.Messages;
using SIM.Application.Result;
using SIM.Domain.Models.Invoice;

namespace SIM.Application.Handlers.Invoices.Command.Delete
{
    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, Result.Result>
    {
        private readonly ISIMContext _context;

        public DeleteInvoiceCommandHandler(ISIMContext context)
        {
            _context = context;
        }

        public async Task<Result.Result> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await GetInvoiceAsync(request, cancellationToken);

            if (invoice is null)
                return Result.Result.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.InvoiceNotFound)));

            _context.Invoices.Remove(invoice);

            await _context.SaveAsync(cancellationToken);

            return Result.Result.SuccessFul();
        }

        private async Task<Invoice> GetInvoiceAsync(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            return await _context.Invoices.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
        
    }
}