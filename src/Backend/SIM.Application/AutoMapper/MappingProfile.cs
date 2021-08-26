using System;
using System.Linq;
using AutoMapper;
using SIM.Application.Handlers.Items.Command.AddNewItem;
using SIM.Application.Handlers.Items.Command.UpdateItem;
using SIM.Application.Handlers.Items.Dto;
using SIM.Domain.Models;
using SIM.Domain.Models.Invoice;
using SIM.Domain.Models.Item;

namespace SIM.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {           
            CreateMap<Item, ItemDto>();
            CreateMap<AddNewItemCommand, Item>();
            CreateMap<UpdateItemCommand, Item>();

        }
    }
}