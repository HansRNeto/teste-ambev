using Ambev.DeveloperEvaluation.Application.Customer.CreateCustomer;
using Ambev.DeveloperEvaluation.Application.Customer.GetCustomer;
using Ambev.DeveloperEvaluation.Application.Customer.ListCustomers;
using Ambev.DeveloperEvaluation.Application.Customer.UpdateCustomer;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.DeleteCustomer;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.GetCustomer;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.ListCustomers;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.UpdateCustomer;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers
{
    /// <summary>
    /// Handles HTTP requests related to customers.
    /// This controller exposes an endpoint for creating a new customer.
    /// </summary>
    /// <remarks>
    /// The <see cref="CustomersController"/> class manages HTTP operations for customer-related actions within the application.
    /// It provides an endpoint to create new customers by accepting a request with customer details and returning the created customer response.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator to handle requests.</param>
        /// <param name="mapper">The mapper to transform DTOs between layers.</param>
        public CustomersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="request">The request containing customer details.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation if needed.</param>
        /// <returns>A response with the created customer data or validation errors.</returns>
        /// <response code="201">Customer successfully created. Returns the customer details in the response body.</response>
        /// <response code="400">Bad Request. Validation failed for the provided customer data.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateCustomerResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateCustomerRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<CreateCustomerCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);

                return Created(string.Empty, new ApiResponseWithData<CreateCustomerResponse>
                {
                    Success = true,
                    Message = "Customer created successfully",
                    Data = _mapper.Map<CreateCustomerResponse>(response)
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Retrieves the details of a specific Customer by its unique identifier.
        /// </summary>
        /// <remarks>
        /// This endpoint allows clients to retrieve detailed information about a Customer using its unique ID.
        /// It validates the request ID and returns a response containing the Customer's data, including customer, branch, 
        /// total amount, and other relevant Customer details. The operation supports error handling in case the Customer ID is invalid 
        /// or not found in the system.
        /// </remarks>
        /// <param name="id">The unique identifier of the Customer to be retrieved.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests during the operation.</param>
        /// <returns>
        /// A response with a status of 200 OK containing the Customer's data if successful. 
        /// A 400 Bad Request response will be returned if the request is invalid, 
        /// and a 404 Not Found response will be returned if the Customer with the provided ID does not exist.
        /// </returns>
        /// <response code="200">Returns the Customer's details successfully retrieved from the database.</response>
        /// <response code="400">If the provided ID is invalid or the request does not meet validation requirements.</response>
        /// <response code="404">If the Customer with the given ID is not found in the system.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetCustomerResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var request = new GetCustomerRequest { Id = id };
                var validator = new GetCustomerRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<GetCustomerCommand>(request.Id);
                var response = await _mediator.Send(command, cancellationToken);

                var result = _mapper.Map<GetCustomerResponse>(response);
                return Ok(result, "Customer retrieved successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>
        /// Retrieves a paginated list of Customers with optional sorting.
        /// </summary>
        /// <remarks>
        /// This endpoint allows clients to retrieve a list of Customers with support for pagination and optional sorting by a specified field.
        /// It validates the request parameters and returns a paginated collection of Customers, including information such as customer, branch, 
        /// total amount, and Customer status. The operation supports error handling for invalid requests or if no Customers are found.
        /// </remarks>
        /// <param name="pageNumber">The number of the page to be retrieved (starting from 1).</param>
        /// <param name="pageSize">The number of records to include in each page.</param>
        /// <param name="sortBy">The field to sort the Customers by (optional).</param>
        /// <param name="sortDirection">The direction of the sorting: 'asc' for ascending or 'desc' for descending (optional).</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests during the operation.</param>
        /// <returns>
        /// A response with a status of 200 OK containing the paginated list of Customers if successful.
        /// A 400 Bad Request response will be returned if the request parameters are invalid,
        /// and a 404 Not Found response will be returned if no Customers are found for the given parameters.
        /// </returns>
        /// <response code="200">Returns the paginated list of Customers successfully retrieved from the database.</response>
        /// <response code="400">If the provided pagination or sorting parameters are invalid or missing.</response>
        /// <response code="404">If no Customers are found matching the provided query parameters.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<List<ListCustomersResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomers(int pageNumber, int pageSize, string? sortBy,
            string? sortDirection, CancellationToken cancellationToken)
        {
            try
            {
                var request = new ListCustomersRequest()
                {
                    PageNumber = pageNumber,
                    SortDirection = sortDirection,
                    SortBy = sortBy,
                    PageSize = pageSize
                };
                var validator = new ListCustomersRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<ListCustomersCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);

                var result = _mapper.Map<List<ListCustomersResponse>>(response);
                return Ok(result, "List customers retrieved successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update a customer.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request">The request containing customer details.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation if needed.</param>
        /// <returns>A response with the updated customer data or validation errors.</returns>
        /// <response code="201">Customer updated successfully. Returns the customer details in the response body.</response>
        /// <response code="400">Bad Request. Validation failed for the provided customer data.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateCustomerResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid id, [FromBody] UpdateCustomerRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var validator = new UpdateCustomerRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<UpdateCustomerCommand>(request);
                command.Id = id;
                var response = await _mediator.Send(command, cancellationToken);

                return Created(string.Empty, new ApiResponseWithData<UpdateCustomerResponse>
                {
                    Success = true,
                    Message = "Customer updated successfully",
                    Data = _mapper.Map<UpdateCustomerResponse>(response)
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes a Customer by its unique identifier.
        /// </summary>
        /// <remarks>
        /// This endpoint validates the provided Customer ID, sends a command to delete the Customer,
        /// and returns an appropriate HTTP response based on the outcome.
        /// 
        /// - Returns 200 OK if the Customer is successfully deleted.
        /// - Returns 400 Bad Request if the request validation fails.
        /// - Returns 404 Not Found if no Customer with the specified ID exists.
        /// 
        /// The Customer deletion operation is executed via the <see cref="IMediator"/> command dispatching mechanism,
        /// following the CQRS (Command Query Responsibility Segregation) pattern.
        /// </remarks>
        /// <param name="id">The unique identifier of the Customer to be deleted.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>
        /// A standardized <see cref="ApiResponse"/> indicating success or failure of the operation.
        /// </returns>
        /// <response code="200">Customer successfully deleted.</response>
        /// <response code="400">Invalid request (e.g., validation errors).</response>
        /// <response code="404">Customer not found for the provided ID.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var request = new DeleteCustomerRequest { Id = id };
                var validator = new DeleteCustomerRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<DeleteCustomerCommand>(request.Id);
                await _mediator.Send(command, cancellationToken);

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Customer deleted successfully"
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}