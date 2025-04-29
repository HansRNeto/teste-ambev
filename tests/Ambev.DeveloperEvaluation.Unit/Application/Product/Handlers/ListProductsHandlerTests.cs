using Ambev.DeveloperEvaluation.Application.Product.ListProducts;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product.Handlers;

public class ListProductsHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ListProductsHandler _handler;

    public ListProductsHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new ListProductsHandler(_productRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Throw_Validation_Exception_When_Command_Is_Invalid()
    {
        var command = new ListProductsCommand
        {
            PageNumber = 0,
            PageSize = 10
        };

        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_Should_Return_Products_When_Command_Is_Valid()
    {
        var command = new ListProductsCommand
        {
            PageNumber = 1,
            PageSize = 10
        };

        var productList = new List<DeveloperEvaluation.Domain.Entities.Product>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Description = "Description 1",
                Price = 10.0m,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        var productResults = new List<ListProductsResult>
        {
            new()
            {
                Id = productList[0].Id,
                Name = "Product 1",
                Description = "Description 1",
                Price = 10.0m,
                IsActive = true,
                CreatedAt = productList[0].CreatedAt,
                UpdatedAt = productList[0].UpdatedAt
            }
        };

        _productRepositoryMock
            .Setup(repo => repo.ListAsync(
                command.PageNumber, command.PageSize, command.SortBy, command.SortDirection, It.IsAny<CancellationToken>()))
            .ReturnsAsync(productList);

        _mapperMock
            .Setup(mapper => mapper.Map<List<ListProductsResult>>(productList))
            .Returns(productResults);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Product 1", result[0].Name);
    }
}
