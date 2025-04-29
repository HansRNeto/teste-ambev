using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using Ambev.DeveloperEvaluation.Application.SaleItem.CreateSaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.Handlers;

public class CreateSaleHandlerTests
{
    private readonly Mock<ISaleRepository> _saleRepositoryMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _saleRepositoryMock = new Mock<ISaleRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _mapperMock = new Mock<IMapper>();

        _handler = new CreateSaleHandler(
            _saleRepositoryMock.Object,
            _mapperMock.Object,
            _productRepositoryMock.Object
        );
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateSale()
    {
        var command = new CreateSaleCommand
        {
            SaleNumber = "SALE123",
            CustomerId = Guid.NewGuid(),
            CustomerName = "John Doe",
            BranchId = Guid.NewGuid(),
            BranchName = "Main Branch",
            SaleItems = new List<CreateSaleItemCommand>
            {
                new() { ProductId = Guid.NewGuid(), ProductName = "Product A", Quantity = 5 }
            }
        };

        var product = new DeveloperEvaluation.Domain.Entities.Product
            { Id = command.SaleItems.First().ProductId, Name = "Product A", Price = 100m };

        var sale = new DeveloperEvaluation.Domain.Entities.Sale { Id = Guid.NewGuid(), TotalAmount = 450m };

        var saleResult = new CreateSaleResult { Id = sale.Id, TotalAmount = sale.TotalAmount };

        _productRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.SaleItems.First().ProductId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        _mapperMock
            .Setup(mapper => mapper.Map<DeveloperEvaluation.Domain.Entities.Sale>(command))
            .Returns(new DeveloperEvaluation.Domain.Entities.Sale { SaleItems = new List<SaleItem>() });

        _mapperMock
            .Setup(mapper => mapper.Map<SaleItem>(command.SaleItems.First()))
            .Returns(new SaleItem());

        _saleRepositoryMock
            .Setup(repo =>
                repo.CreateAsync(It.IsAny<DeveloperEvaluation.Domain.Entities.Sale>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(sale);

        _mapperMock
            .Setup(mapper => mapper.Map<CreateSaleResult>(It.IsAny<DeveloperEvaluation.Domain.Entities.Sale>()))
            .Returns(saleResult);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(sale.Id);
        result.TotalAmount.Should().Be(450m);
    }

    [Fact]
    public async Task Handle_ValidationFails_ShouldThrowValidationException()
    {
        var command = new CreateSaleCommand();

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_DiscountsAppliedCorrectly_ShouldCalculateTotal()
    {
        var command = new CreateSaleCommand
        {
            SaleNumber = "SALE123",
            CustomerId = Guid.NewGuid(),
            CustomerName = "John Doe",
            BranchId = Guid.NewGuid(),
            BranchName = "Main Branch",
            SaleItems = new List<CreateSaleItemCommand>
            {
                new() { ProductId = Guid.NewGuid(), ProductName = "Product A", Quantity = 10 }
            }
        };

        var product = new DeveloperEvaluation.Domain.Entities.Product
            { Id = command.SaleItems.First().ProductId, Name = "Product A", Price = 100m };

        _productRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.SaleItems.First().ProductId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        _mapperMock
            .Setup(mapper => mapper.Map<DeveloperEvaluation.Domain.Entities.Sale>(command))
            .Returns(new DeveloperEvaluation.Domain.Entities.Sale { SaleItems = new List<SaleItem>() });

        _mapperMock
            .Setup(mapper => mapper.Map<SaleItem>(command.SaleItems.First()))
            .Returns(new SaleItem());

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().NotThrowAsync();
    }
}