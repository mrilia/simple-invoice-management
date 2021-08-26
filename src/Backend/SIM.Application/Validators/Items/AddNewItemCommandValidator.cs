using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SIM.Application.Handlers.Items.Command.AddNew;
using SIM.Application.Interfaces;
using SIM.Application.Messages;

namespace SIM.Application.Common.Validator.Items
{
    public class AddNewInvoiceCommandValidator : AbstractValidator<AddNewItemCommand>
    {
        private readonly ISIMContext _context;

        public AddNewInvoiceCommandValidator(ISIMContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage(ResponseMessage.NameIsRequired)
                .NotNull().WithMessage(ResponseMessage.NameIsRequired)
                .MustAsync(NotExists).WithMessage(ResponseMessage.ItemExists);

            RuleFor(dto => dto.Fee)
                .NotEmpty().WithMessage(ResponseMessage.FeeIsRequired)
                .NotNull().WithMessage(ResponseMessage.FeeIsRequired);

        }

        private async Task<bool> NotExists(string nameToCheck, CancellationToken cancellationToken)
        {
            if (await _context.Items.AnyAsync(x => x.Name.Replace(" ", "") == nameToCheck.Replace(" ", ""), cancellationToken))
                return false;

            return true;
        }
    }
}