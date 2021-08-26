using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SIM.Application.Handlers.Invoices.Command.AddNew;
using SIM.Application.Interfaces;
using SIM.Application.Messages;

namespace SIM.Application.Common.Validator.Invoices
{
    public class AddNewInvoiceCommandValidator : AbstractValidator<AddNewInvoiceCommand>
    {
        private readonly ISIMContext _context;

        public AddNewInvoiceCommandValidator(ISIMContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.Number)
                .NotEmpty().WithMessage(ResponseMessage.NumberIsRequired)
                .NotNull().WithMessage(ResponseMessage.NumberIsRequired)
                .MustAsync(UniqueInvoiceNumber).WithMessage(ResponseMessage.InvoiceNumberNotUnique);

            RuleFor(dto => dto.BuyerName)
                .NotEmpty().WithMessage(ResponseMessage.BuyerNameIsRequired)
                .NotNull().WithMessage(ResponseMessage.BuyerNameIsRequired);

            RuleFor(dto => dto)
                .Must(ValidPrices).WithMessage(ResponseMessage.PricesNotValid)
                .Must(ValidRowsPrices).WithMessage(ResponseMessage.PricesNotValid);

        }

        private async Task<bool> UniqueInvoiceNumber(long numberToCheck, CancellationToken cancellationToken)
        {
            if (await _context.Invoices.AnyAsync(x => x.Number == numberToCheck, cancellationToken))
                return false;

            return true;
        }

        private bool ValidPrices(AddNewInvoiceCommand newInvoiceToCheck)
        {
            return newInvoiceToCheck.PayablePrice == newInvoiceToCheck.TotalPrice - newInvoiceToCheck.TotalDiscount;
        }

        private bool ValidRowsPrices(AddNewInvoiceCommand newInvoiceToCheck)
        {
            long rowsTotalPayaplePrice = 0;
            newInvoiceToCheck.Rows.ForEach(row => rowsTotalPayaplePrice += row.PayablePrice);

            long rowsTotalDiscountPrice = 0;
            newInvoiceToCheck.Rows.ForEach(row => rowsTotalDiscountPrice += row.TotalDiscountPrice);

            long rowsTotalPrice = 0;
            newInvoiceToCheck.Rows.ForEach(row => rowsTotalPrice += row.TotalPriceBeforeDiscount);

            return rowsTotalPayaplePrice == newInvoiceToCheck.PayablePrice
                && rowsTotalDiscountPrice == newInvoiceToCheck.TotalDiscount
                && rowsTotalPrice == newInvoiceToCheck.TotalPrice;
        }

    }
}