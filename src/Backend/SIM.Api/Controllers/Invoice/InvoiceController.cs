using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIM.Application.Handlers.Invoices.Command.AddNew;
using SIM.Application.Handlers.Invoices.Command.Delete;
using SIM.Application.Handlers.Invoices.Command.Update;
using SIM.Application.Handlers.Invoices.Queries;
using SIM.Application.Handlers.Invoices.Dto;
using SIM.Application.Messages;

namespace SIM.Api.Controllers.Invoice
{
    public class InvoiceController : BaseController
    {
        private readonly IMediator _mediator;

        public InvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add new invoice
        /// </summary>
        /// <param name="addNewInvoiceCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">if create invoice successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(InvoiceDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        public async Task<IActionResult> AddNew(AddNewInvoiceCommand addNewInvoiceCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(addNewInvoiceCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return Created(Url.Link("GetInvoiceInfo", new {number = result.Data.Number}), result.Data);
        }



        /// <summary>
        /// Invoice Info
        /// </summary>
        /// <param name="number"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Invoice info</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="404">If invoice not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(InvoiceDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{number}", Name = "GetInvoiceInfo")]
        public async Task<IActionResult> Get(long number, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetInvoiceQuery { Number = number }, cancellationToken);

            return result.ApiResult;
        }




        /// <summary>
        /// Invoice Number
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns> New Invoice Number</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(InvoiceDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("NewInvoiceNumber", Name = "GetNewInvoiceNumber")]
        public async Task<IActionResult> GetNewNumber(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetNewInvoiceNumberQuery { }, cancellationToken);

            return result.ApiResult;
        }



        /// <summary>
        /// List Of Invoices 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Invoices list</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="400">If page or limit is overFlow</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(List<InvoiceDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("list")]
        public async Task<IActionResult> GetList(string query, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetInvoicesListQuery { Query = query }, cancellationToken));




        /// <summary>
        /// Delete Invoice
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">if delete successfully </response>
        /// <response code="404">If invoice not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteInvoiceCommand { Id = id }, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }



        /// <summary>
        /// Update  Invoice
        /// </summary>
        /// <param name="updateInvoiceCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">if update invoice successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="404">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateInvoiceCommand updateInvoiceCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updateInvoiceCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }
    }
}