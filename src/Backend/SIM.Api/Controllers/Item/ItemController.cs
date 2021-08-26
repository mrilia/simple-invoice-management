using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIM.Application.Handlers.Items.Command.AddNew;
using SIM.Application.Handlers.Items.Command.Delete;
using SIM.Application.Handlers.Items.Command.Update;
using SIM.Application.Handlers.Items.Queries;
using SIM.Application.Handlers.Items.Dto;
using SIM.Application.Messages;

namespace SIM.Api.Controllers.Item
{
    public class ItemController : BaseController
    {
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add new item
        /// </summary>
        /// <param name="addNewItemCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">if create item successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ItemDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        public async Task<IActionResult> AddNew(AddNewItemCommand addNewItemCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(addNewItemCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return Created(Url.Link("GetItemInfo", new {id = result.Data.Id}), result.Data);
        }



        /// <summary>
        /// Item Info
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Item info</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="404">If item not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ItemDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}", Name = "GetItemInfo")]
        public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetItemQuery { Id = id }, cancellationToken);

            return result.ApiResult;
        }



        /// <summary>
        /// List Of Items 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Items list</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="400">If page or limit is overFlow</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(List<ItemDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("list")]
        public async Task<IActionResult> GetList(string query, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetItemsListQuery { Query = query }, cancellationToken));




        /// <summary>
        /// Delete Item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">if delete successfully </response>
        /// <response code="404">If item not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteItemCommand { Id = id }, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }



        /// <summary>
        /// Update  Item
        /// </summary>
        /// <param name="updateItemCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">if update item successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="404">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateItemCommand updateItemCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updateItemCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }
    }
}