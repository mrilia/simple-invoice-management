using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SIM.Application.Handlers.Items.Command.AddNewItem;
using SIM.Application.Interfaces;
using SIM.Application.Messages;

namespace SIM.Application.Common.Validator.Doors
{
    public class AddNewItemCommandValidator : AbstractValidator<AddNewItemCommand>
    {
        private readonly ISIMContext _context;

        public AddNewItemCommandValidator(ISIMContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage(ResponseMessage.NameIsRequired)
                .NotNull().WithMessage(ResponseMessage.NameIsRequired);

            RuleFor(dto => dto.Fee)
                .NotEmpty().WithMessage(ResponseMessage.FeeIsRequired)
                .NotNull().WithMessage(ResponseMessage.FeeIsRequired);

        }

    }
}