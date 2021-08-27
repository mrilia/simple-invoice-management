using System;
using System.Linq;
using AutoMapper;
using SIM.Application.Handlers.Items.Command.AddNew;
using SIM.Application.Handlers.Items.Command.Update;
using SIM.Application.Handlers.Items.Dto;
using SIM.Application.Handlers.Invoices.Command.AddNew;
using SIM.Application.Handlers.Invoices.Command.Update;
using SIM.Application.Handlers.Invoices.Dto;
using SIM.Domain.Models;
using SIM.Domain.Models.Invoice;
using SIM.Domain.Models.Item;
using System.Collections.Generic;

namespace SIM.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Item, ItemDto>();
            CreateMap<AddNewItemCommand, Item>();
            CreateMap<UpdateItemCommand, Item>();

            CreateMap<Invoice, InvoiceDto>();
            CreateMap<AddNewInvoiceCommand, Invoice>();
            CreateMap<UpdateInvoiceCommand, Invoice>();
            CreateMap<InvoiceRow, InvoiceRowDto>();
            CreateMap<RowDto, InvoiceRow>();
            CreateMap<RowDtoToUpdate, InvoiceRow>();
            CreateMap<NumberDto, InvoiceNumberDto>();
        }
    }
}