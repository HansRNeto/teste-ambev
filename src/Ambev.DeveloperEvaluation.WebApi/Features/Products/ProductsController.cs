using Ambev.DeveloperEvaluation.Application.Product.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Product.GetProduct;
using Ambev.DeveloperEvaluation.Application.Product.ListProducts;
using Ambev.DeveloperEvaluation.Application.Product.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Product.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

/// <summary>
/// Controller responsible for handling product-related operations.
/// </summary>
/// <remarks>
/// Provides endpoints to manage products in the system, such as creating a new product.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductsController"/> class.
    /// </summary>
    /// <param name="mediator">Mediator for sending commands and queries.</param>
    /// <param name="mapper">Mapper for converting between models and DTOs.</param>
    public ProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="request">The product creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Returns the newly created product information.</returns>
    /// <response code="201">Product successfully created.</response>
    /// <response code="400">Validation error or bad request.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var validator = new CreateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateProductCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
            {
                Success = true,
                Message = "Product created successfully",
                Data = _mapper.Map<CreateProductResponse>(response)
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Retrieves the details of a specific Product by its unique identifier.
    /// </summary>
    /// <remarks>
    /// This endpoint allows clients to retrieve detailed information about a Product using its unique ID.
    /// It validates the request ID and returns a response containing the Product's data, including Product, branch, 
    /// total amount, and other relevant Product details. The operation supports error handling in case the Product ID is invalid 
    /// or not found in the system.
    /// </remarks>
    /// <param name="id">The unique identifier of the Product to be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests during the operation.</param>
    /// <returns>
    /// A response with a status of 200 OK containing the Product's data if successful. 
    /// A 400 Bad Request response will be returned if the request is invalid, 
    /// and a 404 Not Found response will be returned if the Product with the provided ID does not exist.
    /// </returns>
    /// <response code="200">Returns the Product's details successfully retrieved from the database.</response>
    /// <response code="400">If the provided ID is invalid or the request does not meet validation requirements.</response>
    /// <response code="404">If the Product with the given ID is not found in the system.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var request = new GetProductRequest { Id = id };
            var validator = new GetProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetProductCommand>(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            var result = _mapper.Map<GetProductResponse>(response);
            return Ok(result, "Product retrieved successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Retrieves a paginated list of Products with optional sorting.
    /// </summary>
    /// <remarks>
    /// This endpoint allows clients to retrieve a list of Products with support for pagination and optional sorting by a specified field.
    /// It validates the request parameters and returns a paginated collection of Products, including information such as Product, branch, 
    /// total amount, and Product status. The operation supports error handling for invalid requests or if no Products are found.
    /// </remarks>
    /// <param name="pageNumber">The number of the page to be retrieved (starting from 1).</param>
    /// <param name="pageSize">The number of records to include in each page.</param>
    /// <param name="sortBy">The field to sort the Products by (optional).</param>
    /// <param name="sortDirection">The direction of the sorting: 'asc' for ascending or 'desc' for descending (optional).</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests during the operation.</param>
    /// <returns>
    /// A response with a status of 200 OK containing the paginated list of Products if successful.
    /// A 400 Bad Request response will be returned if the request parameters are invalid,
    /// and a 404 Not Found response will be returned if no Products are found for the given parameters.
    /// </returns>
    /// <response code="200">Returns the paginated list of Products successfully retrieved from the database.</response>
    /// <response code="400">If the provided pagination or sorting parameters are invalid or missing.</response>
    /// <response code="404">If no Products are found matching the provided query parameters.</response>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<List<ListProductsResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProducts(int pageNumber, int pageSize, string? sortBy,
        string? sortDirection, CancellationToken cancellationToken)
    {
        try
        {
            var request = new ListProductsRequest()
            {
                PageNumber = pageNumber,
                SortDirection = sortDirection,
                SortBy = sortBy,
                PageSize = pageSize
            };
            var validator = new ListProductsRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<ListProductsCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            var result = _mapper.Map<List<ListProductsResponse>>(response);
            return Ok(result, "List products retrieved successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Update a Product.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request">The request containing Product details.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation if needed.</param>
    /// <returns>A response with the updated Product data or validation errors.</returns>
    /// <response code="201">Product updated successfully. Returns the Product details in the response body.</response>
    /// <response code="400">Bad Request. Validation failed for the provided Product data.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var validator = new UpdateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateProductCommand>(request);
            command.Id = id;
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<UpdateProductResponse>
            {
                Success = true,
                Message = "Product updated successfully",
                Data = _mapper.Map<UpdateProductResponse>(response)
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Deletes a Product by its unique identifier.
    /// </summary>
    /// <remarks>
    /// This endpoint validates the provided Product ID, sends a command to delete the Product,
    /// and returns an appropriate HTTP response based on the outcome.
    /// 
    /// - Returns 200 OK if the Product is successfully deleted.
    /// - Returns 400 Bad Request if the request validation fails.
    /// - Returns 404 Not Found if no Product with the specified ID exists.
    /// 
    /// The Product deletion operation is executed via the <see cref="IMediator"/> command dispatching mechanism,
    /// following the CQRS (Command Query Responsibility Segregation) pattern.
    /// </remarks>
    /// <param name="id">The unique identifier of the Product to be deleted.</param>
    /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
    /// <returns>
    /// A standardized <see cref="ApiResponse"/> indicating success or failure of the operation.
    /// </returns>
    /// <response code="200">Product successfully deleted.</response>
    /// <response code="400">Invalid request (e.g., validation errors).</response>
    /// <response code="404">Product not found for the provided ID.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var request = new DeleteProductRequest { Id = id };
            var validator = new DeleteProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteProductCommand>(request.Id);
            await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Product deleted successfully"
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}