using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SIM.Application.Handlers.Items.Dto;
using SIM.Application.Interfaces;
using SIM.Application.Result;
using SIM.Domain.Models.Item;

namespace SIM.Application.Handlers.Items.Command.AddNew
{
    public class AddNewItemCommandHandler : IRequestHandler<AddNewItemCommand, Result<ItemDto>>
    {
        private readonly ISIMContext _context;
        private readonly IMapper _mapper;

        public AddNewItemCommandHandler(ISIMContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ItemDto>> Handle(AddNewItemCommand request, CancellationToken cancellationToken)
        {
            var itemToAdd = _mapper.Map<Item>(request);

            await _context.Items.AddAsync(itemToAdd, cancellationToken);
            await _context.SaveAsync(cancellationToken);

            return Result<ItemDto>.SuccessFul(_mapper.Map<ItemDto>(itemToAdd));
        }
    }
}