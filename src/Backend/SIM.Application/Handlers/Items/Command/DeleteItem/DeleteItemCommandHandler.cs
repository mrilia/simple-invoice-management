using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIM.Application.Interfaces;
using SIM.Application.Messages;
using SIM.Application.Result;
using SIM.Domain.Models.Item;

namespace SIM.Application.Handlers.Items.Command.DeleteItem
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, Result.Result>
    {
        private readonly ISIMContext _context;

        public DeleteItemCommandHandler(ISIMContext context)
        {
            _context = context;
        }

        public async Task<Result.Result> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var item = await GetItemAsync(request, cancellationToken);

            if (item is null)
                return Result.Result.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.ItemNotFound)));

            _context.Items.Remove(item);

            await _context.SaveAsync(cancellationToken);

            return Result.Result.SuccessFul();
        }

        private async Task<Item> GetItemAsync(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            return await _context.Items.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
        
    }
}