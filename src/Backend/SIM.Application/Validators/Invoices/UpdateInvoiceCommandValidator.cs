using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SIM.Application.Handlers.Invoices.Command.Update;
using SIM.Application.Interfaces;
using SIM.Application.Messages;

namespace SIM.Application.Common.Validator.Invoices
{
    public class UpdateInvoiceCommandValidator : AbstractValidator<UpdateInvoiceCommand>
    {
        private readonly ISIMContext _context;

        public UpdateInvoiceCommandValidator(ISIMContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.Id)
                .NotEmpty().WithMessage(ResponseMessage.IdIsRequired)
                .NotNull().WithMessage(ResponseMessage.IdIsRequired)
                .MustAsync(Exists).WithMessage(ResponseMessage.ItemIdNotExists);

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

        private bool ValidPrices(UpdateInvoiceCommand invoiceToCheck)
        {
            return invoiceToCheck.PayablePrice == invoiceToCheck.TotalPrice - invoiceToCheck.TotalDiscount;
        }

        private bool ValidRowsPrices(UpdateInvoiceCommand invoiceToCheck)
        {
            long rowsTotalPayaplePrice = 0;
            invoiceToCheck.Rows.ForEach(row => rowsTotalPayaplePrice += row.PayablePrice);

            long rowsTotalDiscountPrice = 0;
            invoiceToCheck.Rows.ForEach(row => rowsTotalDiscountPrice += row.TotalDiscountPrice);

            long rowsTotalPrice = 0;
            invoiceToCheck.Rows.ForEach(row => rowsTotalPrice += row.TotalPriceBeforeDiscount);

            return rowsTotalPayaplePrice == invoiceToCheck.PayablePrice
                && rowsTotalDiscountPrice == invoiceToCheck.TotalDiscount
                && rowsTotalPrice == invoiceToCheck.TotalPrice;
        }

        private async Task<bool> Exists(long idToCheck, CancellationToken cancellationToken)
        {
            if (!await _context.Items.AnyAsync(x => x.Id == idToCheck, cancellationToken))
                return false;

            return true;
        }
    }
}