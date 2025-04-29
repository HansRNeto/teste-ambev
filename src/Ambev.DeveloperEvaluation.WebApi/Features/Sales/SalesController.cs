using Ambev.DeveloperEvaluation.Application.Sale.GetSale;
using Ambev.DeveloperEvaluation.Application.Sale.ListSales;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    /// <summary>
    /// Controller responsible for handling sales-related operations. 
    /// Provides an endpoint for creating a new sale in the system.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator to handle commands and queries.</param>
        /// <param name="mapper">Mapper to convert between DTOs and domain models.</param>
        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new sale in the system based on the provided <see cref="CreateSaleRequest"/>.
        /// The request is validated before processing and the sale is persisted in the system.
        /// </summary>
        /// <remarks>
        /// This endpoint allows Sales to create a new sale, including details like sale number, 
        /// customer information, branch information, and sale amount. 
        /// The request body should contain a valid <see cref="CreateSaleRequest"/> with necessary details.
        /// 
        /// If validation fails, the endpoint returns a 400 Bad Request with details about the validation errors.
        /// On success, it returns a 201 Created response with the created sale data.
        /// </remarks>
        /// <param name="request">The request object containing details of the sale to be created.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation if needed.</param>
        /// <returns>
        /// A <see cref="IActionResult"/> indicating the result of the operation:
        /// - 201 Created: The sale was successfully created.
        /// - 400 Bad Request: Validation failed or an error occurred during processing.
        /// </returns>
        /// <response code="201">Sale successfully created.</response>
        /// <response code="400">The request was invalid or an error occurred during processing.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateSaleRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<CreateSaleCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);

                return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
                {
                    Success = true,
                    Message = "Sale created successfully",
                    Data = _mapper.Map<CreateSaleResponse>(response)
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Retrieves the details of a specific sale by its unique identifier.
        /// </summary>
        /// <remarks>
        /// This endpoint allows clients to retrieve detailed information about a sale using its unique ID.
        /// It validates the request ID and returns a response containing the sale's data, including customer, branch, 
        /// total amount, and other relevant sale details. The operation supports error handling in case the sale ID is invalid 
        /// or not found in the system.
        /// </remarks>
        /// <param name="id">The unique identifier of the sale to be retrieved.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests during the operation.</param>
        /// <returns>
        /// A response with a status of 200 OK containing the sale's data if successful. 
        /// A 400 Bad Request response will be returned if the request is invalid, 
        /// and a 404 Not Found response will be returned if the sale with the provided ID does not exist.
        /// </returns>
        /// <response code="200">Returns the sale's details successfully retrieved from the database.</response>
        /// <response code="400">If the provided ID is invalid or the request does not meet validation requirements.</response>
        /// <response code="404">If the sale with the given ID is not found in the system.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetSaleRequest { Id = id };
            var validator = new GetSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetSaleCommand>(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            var result = _mapper.Map<GetSaleResponse>(response);
            return Ok(result, "Sale retrieved successfully");
        }

        /// <summary>
        /// Retrieves a paginated list of sales with optional sorting.
        /// </summary>
        /// <remarks>
        /// This endpoint allows clients to retrieve a list of sales with support for pagination and optional sorting by a specified field.
        /// It validates the request parameters and returns a paginated collection of sales, including information such as customer, branch, 
        /// total amount, and sale status. The operation supports error handling for invalid requests or if no sales are found.
        /// </remarks>
        /// <param name="pageNumber">The number of the page to be retrieved (starting from 1).</param>
        /// <param name="pageSize">The number of records to include in each page.</param>
        /// <param name="sortBy">The field to sort the sales by (optional).</param>
        /// <param name="sortDirection">The direction of the sorting: 'asc' for ascending or 'desc' for descending (optional).</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests during the operation.</param>
        /// <returns>
        /// A response with a status of 200 OK containing the paginated list of sales if successful.
        /// A 400 Bad Request response will be returned if the request parameters are invalid,
        /// and a 404 Not Found response will be returned if no sales are found for the given parameters.
        /// </returns>
        /// <response code="200">Returns the paginated list of sales successfully retrieved from the database.</response>
        /// <response code="400">If the provided pagination or sorting parameters are invalid or missing.</response>
        /// <response code="404">If no sales are found matching the provided query parameters.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<List<ListSalesResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSales(int pageNumber, int pageSize, string? sortBy,
            string? sortDirection, CancellationToken cancellationToken)
        {
            try
            {
                var request = new ListSalesRequest()
                {
                    PageNumber = pageNumber,
                    SortDirection = sortDirection,
                    SortBy = sortBy,
                    PageSize = pageSize
                };
                var validator = new ListSalesRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<ListSalesCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);

                var result = _mapper.Map<List<ListSalesResponse>>(response);
                return Ok(result, "List sales retrieved successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes a sale by its unique identifier.
        /// </summary>
        /// <remarks>
        /// This endpoint validates the provided sale ID, sends a command to delete the sale,
        /// and returns an appropriate HTTP response based on the outcome.
        /// 
        /// - Returns 200 OK if the sale is successfully deleted.
        /// - Returns 400 Bad Request if the request validation fails.
        /// - Returns 404 Not Found if no sale with the specified ID exists.
        /// 
        /// The sale deletion operation is executed via the <see cref="IMediator"/> command dispatching mechanism,
        /// following the CQRS (Command Query Responsibility Segregation) pattern.
        /// </remarks>
        /// <param name="id">The unique identifier of the sale to be deleted.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>
        /// A standardized <see cref="ApiResponse"/> indicating success or failure of the operation.
        /// </returns>
        /// <response code="200">Sale successfully deleted.</response>
        /// <response code="400">Invalid request (e.g., validation errors).</response>
        /// <response code="404">Sale not found for the provided ID.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var request = new DeleteSaleRequest { Id = id };
                var validator = new DeleteSaleRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<DeleteSaleCommand>(request.Id);
                await _mediator.Send(command, cancellationToken);

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Sale deleted successfully"
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}