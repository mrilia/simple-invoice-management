using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SIM.Application.Handlers.Items.Command.Delete;
using SIM.Application.Handlers.Items.Command.Update;
using SIM.Application.Handlers.Items.Dto;
using SIM.Application.Handlers.Items.Queries;
using SIM.Application.Messages;
using SIM.Application.Result;
using Xunit;

namespace SepidStar.UnitTest.Door
{
    public class ItemController
    {

        [Fact]
        public async Task WhenInvalidIdSend_returnNotFound()
        {
            var mockData = new Mock<IMediator>();

            mockData.Setup(x => x.Send(It.IsAny<GetItemQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    Result<ItemDto>.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.ItemNotFound))));

            using var controller = new BaseConfiguration().WithMediatorService(mockData.Object).BuildItemController();

            var result = await controller.Get(It.IsAny<long>(),It.IsAny<CancellationToken>());

            var objectResult = Assert.IsType<NotFoundObjectResult>(result);

            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

    }
}