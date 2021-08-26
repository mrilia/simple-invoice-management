using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIM.Application.Interfaces;
using SIM.Application.Messages;
using SIM.Application.Result;
using SIM.Domain.Models.Item;

namespace SIM.Application.Handlers.Items.Command.UpdateItem
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Result.Result>
    {
        private readonly ISIMContext _context;
        private readonly IMapper _mapper;

        public UpdateItemCommandHandler(ISIMContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result.Result> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var item = await GetItemAsync(request, cancellationToken);

            if (item is null)
                return Result.Result.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.ItemNotFound)));

            _mapper.Map(request, item);

            await _context.SaveAsync(cancellationToken);

            return Result.Result.SuccessFul();
        }

        private async Task<Item> GetItemAsync(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            return await _context.Items.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
        
    }
}