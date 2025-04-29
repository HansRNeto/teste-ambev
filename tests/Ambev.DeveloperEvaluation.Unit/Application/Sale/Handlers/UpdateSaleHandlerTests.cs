using Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.Handlers;

public class UpdateSaleHandlerTests
{
    private readonly Mock<ISaleRepository> _saleRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdateSaleHandler _handler;

    public UpdateSaleHandlerTests()
    {
        _saleRepositoryMock = new Mock<ISaleRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new UpdateSaleHandler(_saleRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldUpdateSaleSuccessfully()
    {
        var command = new UpdateSaleCommand
        {
            Id = Guid.NewGuid(),
            SaleNumber = "SALE123",
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            CustomerName = "CustomerName",
            BranchName = "BranchName"
        };

        var existingSale = new DeveloperEvaluation.Domain.Entities.Sale
        {
            Id = command.Id,
            SaleNumber = command.SaleNumber,
            CustomerId = command.CustomerId,
            BranchId = command.BranchId
        };

        var updatedSale = new DeveloperEvaluation.Domain.Entities.Sale
        {
            Id = command.Id,
            SaleNumber = command.SaleNumber,
            CustomerId = command.CustomerId,
            BranchId = command.BranchId
        };

        var expectedResult = new UpdateSaleResult
        {
            Id = updatedSale.Id,
            SaleNumber = updatedSale.SaleNumber
        };

        _saleRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingSale);

        _mapperMock
            .Setup(mapper => mapper.Map<DeveloperEvaluation.Domain.Entities.Sale>(command))
            .Returns(updatedSale);

        _mapperMock
            .Setup(mapper => mapper.Map<UpdateSaleResult>(updatedSale))
            .Returns(expectedResult);

        _saleRepositoryMock
            .Setup(repo => repo.UpdateAsync(updatedSale, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedSale);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(command.Id);
        result.SaleNumber.Should().Be(command.SaleNumber);

        _saleRepositoryMock.Verify(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
        _saleRepositoryMock.Verify(repo => repo.UpdateAsync(updatedSale, It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<DeveloperEvaluation.Domain.Entities.Sale>(command), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<UpdateSaleResult>(updatedSale), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistingSale_ShouldThrowBadHttpRequestException()
    {
        var command = new UpdateSaleCommand
        {
            Id = Guid.NewGuid(),
            SaleNumber = "SALE123",
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            CustomerName = "CustomerName",
            BranchName = "BranchName"
        };

        _saleRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((DeveloperEvaluation.Domain.Entities.Sale)null!);

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<BadHttpRequestException>()
            .WithMessage($"Sale with ID {command.Id} not found.");

        _saleRepositoryMock.Verify(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
        _saleRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<DeveloperEvaluation.Domain.Entities.Sale>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ShouldThrowValidationException()
    {
        var command = new UpdateSaleCommand
        {
            Id = Guid.Empty,
            SaleNumber = "",
            CustomerId = Guid.Empty,
            BranchId = Guid.Empty
        };

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ValidationException>();

        _saleRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
            Times.Never);
        _saleRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<DeveloperEvaluation.Domain.Entities.Sale>(), It.IsAny<CancellationToken>()),
            Times.Never);
        _mapperMock.Verify(m => m.Map<DeveloperEvaluation.Domain.Entities.Sale>(It.IsAny<UpdateSaleCommand>()), Times.Never);
        _mapperMock.Verify(m => m.Map<UpdateSaleResult>(It.IsAny<DeveloperEvaluation.Domain.Entities.Sale>()), Times.Never);
    }
}