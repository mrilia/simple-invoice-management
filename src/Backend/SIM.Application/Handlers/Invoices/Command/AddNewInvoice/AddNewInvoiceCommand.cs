using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SIM.Application.Handlers.Invoices.Dto;
using SIM.Application.Interfaces;
using SIM.Application.Result;
using SIM.Domain.Models.Invoice;

namespace SIM.Application.Handlers.Invoices.Command.AddNew
{
    public class AddNewInvoiceCommand : IRequest<Result<InvoiceDto>>
    {
        public string CreatedOn { get; set; }
        public string BuyerName { get; set; }
        public long Number { get; set; }
        public string Description { get; set; }
        public long TotalPrice { get; set; }
        public long TotalDiscount { get; set; }
        public long PayablePrice { get; set; }

        public List<RowDto> InvoiceRows { get; set; }

    }
    public class RowDto
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public long Fee { get; set; }
        public int DiscountPercent { get; set; }
        public long FeeAfterDiscount { get; set; }
        public long TotalPriceBeforeDiscount { get; set; }
        public long PayablePrice { get; set; }
        public long FeeDiscountPrice { get; set; }
        public long TotalDiscountPrice { get; set; }
    }
}
