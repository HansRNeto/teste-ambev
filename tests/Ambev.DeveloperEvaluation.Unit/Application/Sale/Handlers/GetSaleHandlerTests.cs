using Ambev.DeveloperEvaluation.Application.Sale.GetSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.Handlers;

public class GetSaleHandlerTests
{
    private readonly Mock<ISaleRepository> _saleRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetSaleHandler _handler;

    public GetSaleHandlerTests()
    {
        _saleRepositoryMock = new Mock<ISaleRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetSaleHandler(_saleRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldReturnSaleSuccessfully()
    {
        var command = new GetSaleCommand
        {
            Id = Guid.NewGuid()
        };

        var saleEntity = new DeveloperEvaluation.Domain.Entities.Sale
        {
            Id = command.Id,
            SaleNumber = "SALE123",
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid()
        };

        var expectedResult = new GetSaleResult
        {
            Id = saleEntity.Id,
            SaleNumber = saleEntity.SaleNumber,
            CustomerId = saleEntity.CustomerId,
            BranchId = saleEntity.BranchId
        };

        _saleRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(saleEntity);

        _mapperMock
            .Setup(mapper => mapper.Map<GetSaleResult>(saleEntity))
            .Returns(expectedResult);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(expectedResult.Id);
        result.SaleNumber.Should().Be(expectedResult.SaleNumber);
        result.CustomerId.Should().Be(expectedResult.CustomerId);
        result.BranchId.Should().Be(expectedResult.BranchId);

        _saleRepositoryMock.Verify(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<GetSaleResult>(saleEntity), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistingSale_ShouldThrowKeyNotFoundException()
    {
        var command = new GetSaleCommand
        {
            Id = Guid.NewGuid()
        };

        _saleRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((DeveloperEvaluation.Domain.Entities.Sale)null!);

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Sale with ID {command.Id} not found");

        _saleRepositoryMock.Verify(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<GetSaleResult>(It.IsAny<DeveloperEvaluation.Domain.Entities.Sale>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ShouldThrowValidationException()
    {
        var command = new GetSaleCommand
        {
            Id = Guid.Empty
        };

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ValidationException>();

        _saleRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
            Times.Never);
        _mapperMock.Verify(m => m.Map<GetSaleResult>(It.IsAny<DeveloperEvaluation.Domain.Entities.Sale>()),
            Times.Never);
    }
}