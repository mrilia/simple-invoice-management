using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SIM.Application.Handlers.Items.Dto;
using SIM.Application.Interfaces;
using SIM.Domain.Models.Item;

namespace SIM.Application.Handlers.Items.Queries
{
    public class GetItemsListQuery : IRequest<List<ItemDto>>
    {
        public string Query { get; set; }
    }


    public class GetItemListQueryHandler : IRequestHandler<GetItemsListQuery, List<ItemDto>>
    {
        private readonly ISIMContext _context;
        private readonly IMapper _mapper;

        public GetItemListQueryHandler(ISIMContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ItemDto>> Handle(GetItemsListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Item> items = _context.Items;

            if (!string.IsNullOrWhiteSpace(request.Query))
            {
                items = items.Where(x => x.Name.Contains(request.Query));
            }

            return _mapper.Map<List<ItemDto>>(await items.ToListAsync(cancellationToken));
        }
    }
}