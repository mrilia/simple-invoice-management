using MediatR;
using SIM.Application.Result;

namespace SIM.Application.Handlers.Items.Command.Update
{
    public class UpdateItemCommand : IRequest<Result.Result>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Fee { get; set; }
    }
}