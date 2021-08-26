using MediatR;
using SIM.Application.Result;

namespace SIM.Application.Handlers.Invoices.Command.Delete
{
    public class DeleteInvoiceCommand : IRequest<Result.Result>
    {
        public long Id { get; set; }
    }
}