using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SIM.Application.Handlers.Items.Dto;
using SIM.Application.Interfaces;
using SIM.Application.Result;
using SIM.Domain.Models.Item;

namespace SIM.Application.Handlers.Items.Command.AddNewItem
{
    public class AddNewItemCommand : IRequest<Result<ItemDto>>
    {
        public string Name { get; set; }
        public long Fee { get; set; }
    }
}
