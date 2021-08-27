using MediatR;
using Moq;
using SIM.Api.Controllers;
using SIM.Api.Controllers.Item;


namespace SepidStar.UnitTest
{
    public class BaseConfiguration
    {
        private IMediator _mediator;

        public BaseConfiguration()
        {
            _mediator = new Mock<IMediator>().Object;
        }

        internal BaseConfiguration WithMediatorService(IMediator mediator)
        {
            _mediator = mediator;
            return this;
        }

        internal ItemController BuildItemController() => new(_mediator);

    }
}