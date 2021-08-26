using MediatR;
using SIM.Application.Result;
using System.Collections.Generic;

namespace SIM.Application.Handlers.Invoices.Command.Update
{
    public class UpdateInvoiceCommand : IRequest<Result.Result>
    {
        public long Id { get; set; }
        public string CreatedOn { get; set; }
        public string BuyerName { get; set; }
        public long Number { get; set; }
        public string Description { get; set; }
        public long TotalPrice { get; set; }
        public long TotalDiscount { get; set; }
        public long PayablePrice { get; set; }

        public List<RowDto> Rows { get; set; }
    }

    public class RowDto
    {
        public long Id { get; set; }
        public long InvoiceId { get; set; }
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