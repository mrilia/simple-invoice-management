using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIM.Application.Handlers.Items.Dto;
using SIM.Application.Interfaces;
using SIM.Application.Messages;
using SIM.Application.Result;

namespace SIM.Application.Handlers.Items.Queries
{
    public class GetItemQuery : IRequest<Result<ItemDto>>
    {
        public long Id { get; set; }
    }

    public class GetItemQueryHandler : IRequestHandler<GetItemQuery, Result<ItemDto>>
    {
        private readonly ISIMContext _context;
        private readonly IMapper _mapper;

        public GetItemQueryHandler(ISIMContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ItemDto>> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.SingleOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken);

            if (item is null)
                return Result<ItemDto>.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.ItemNotFound)));

            return Result<ItemDto>.SuccessFul(_mapper.Map<ItemDto>(item));
        }
    }
}