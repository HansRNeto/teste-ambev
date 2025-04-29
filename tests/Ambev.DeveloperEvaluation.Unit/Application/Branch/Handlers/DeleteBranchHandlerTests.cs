using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branch.Handlers;

public class DeleteBranchHandlerTests
{
    private readonly Mock<IBranchRepository> _branchRepositoryMock;
    private readonly DeleteBranchHandler _handler;

    public DeleteBranchHandlerTests()
    {
        _branchRepositoryMock = new Mock<IBranchRepository>();
        _handler = new DeleteBranchHandler(_branchRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldReturnSuccessResponse()
    {
        var requestId = Guid.NewGuid();
        var command = new DeleteBranchCommand { Id = requestId };
        _branchRepositoryMock
            .Setup(repo => repo.DeleteAsync(It.Is<Guid>(id => id == requestId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_InvalidRequest_ShouldThrowValidationException()
    {
        var command = new DeleteBranchCommand();

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_BranchNotFound_ShouldThrowKeyNotFoundException()
    {
        var requestId = Guid.NewGuid();
        var command = new DeleteBranchCommand { Id = requestId };
        _branchRepositoryMock
            .Setup(repo => repo.DeleteAsync(It.Is<Guid>(id => id == requestId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Branch with ID {requestId} not found");
    }
}