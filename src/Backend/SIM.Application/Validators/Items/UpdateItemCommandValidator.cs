using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SIM.Application.Handlers.Items.Command.Update;
using SIM.Application.Interfaces;
using SIM.Application.Messages;

namespace SIM.Application.Common.Validator.Items
{
    public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
    {
        private readonly ISIMContext _context;

        public UpdateItemCommandValidator(ISIMContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.Id)
                .NotEmpty().WithMessage(ResponseMessage.IdIsRequired)
                .NotNull().WithMessage(ResponseMessage.IdIsRequired)
                .MustAsync(Exists).WithMessage(ResponseMessage.ItemIdNotExists);

            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage(ResponseMessage.NameIsRequired)
                .NotNull().WithMessage(ResponseMessage.NameIsRequired);

            RuleFor(dto => dto.Fee)
                            .NotEmpty().WithMessage(ResponseMessage.FeeIsRequired)
                            .NotNull().WithMessage(ResponseMessage.FeeIsRequired);

        }

        private async Task<bool> Exists(long idToCheck, CancellationToken cancellationToken)
        {
            if (!await _context.Items.AnyAsync(x => x.Id == idToCheck, cancellationToken))
                return false;

            return true;
        }
    }
}