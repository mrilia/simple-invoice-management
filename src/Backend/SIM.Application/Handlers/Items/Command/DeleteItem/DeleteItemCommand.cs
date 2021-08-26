using MediatR;
using SIM.Application.Result;

namespace SIM.Application.Handlers.Items.Command.Delete
{
    public class DeleteItemCommand : IRequest<Result.Result>
    {
        public long Id { get; set; }
    }
}