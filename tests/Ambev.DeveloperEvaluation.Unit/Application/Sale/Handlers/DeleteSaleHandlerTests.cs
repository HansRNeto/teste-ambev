using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.Handlers;

public class DeleteSaleHandlerTests
{
    private readonly Mock<ISaleRepository> _saleRepositoryMock;
    private readonly DeleteSaleHandler _handler;

    public DeleteSaleHandlerTests()
    {
        _saleRepositoryMock = new Mock<ISaleRepository>();
        _handler = new DeleteSaleHandler(_saleRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldDeleteSaleSuccessfully()
    {
        var command = new DeleteSaleCommand { Id = Guid.NewGuid() };

        _saleRepositoryMock
            .Setup(repo => repo.DeleteAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var response = await _handler.Handle(command, CancellationToken.None);

        response.Should().NotBeNull();
        response.Success.Should().BeTrue();

        _saleRepositoryMock.Verify(repo => repo.DeleteAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistingSale_ShouldThrowKeyNotFoundException()
    {
        var command = new DeleteSaleCommand { Id = Guid.NewGuid() };

        _saleRepositoryMock
            .Setup(repo => repo.DeleteAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Sale with ID {command.Id} not found");

        _saleRepositoryMock.Verify(repo => repo.DeleteAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ShouldThrowValidationException()
    {
        var command = new DeleteSaleCommand { Id = Guid.Empty };

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ValidationException>();

        _saleRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}