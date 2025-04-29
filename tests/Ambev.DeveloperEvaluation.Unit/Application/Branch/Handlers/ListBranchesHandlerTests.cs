using Ambev.DeveloperEvaluation.Application.Branch.ListBranches;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branch.Handlers
{
    public class ListBranchesHandlerTests
    {
        private readonly Mock<IBranchRepository> _branchRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ListBranchesHandler _handler;

        public ListBranchesHandlerTests()
        {
            _branchRepositoryMock = new Mock<IBranchRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new ListBranchesHandler(_branchRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            var command = new ListBranchesCommand
            {
                PageNumber = 0,
                PageSize = 10
            };

            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldReturnMappedBranches_WhenCommandIsValid()
        {
            var command = new ListBranchesCommand
            {
                PageNumber = 1,
                PageSize = 10
            };

            var branches = new List<DeveloperEvaluation.Domain.Entities.Branch> { new() { Id = Guid.NewGuid(), Name = "Branch 1" } };

            _branchRepositoryMock
                .Setup(repo => repo.ListAsync(command.PageNumber, command.PageSize, command.SortBy, command.SortDirection, It.IsAny<CancellationToken>()))
                .ReturnsAsync(branches);

            _mapperMock
                .Setup(mapper => mapper.Map<List<ListBranchesResult>>(branches))
                .Returns([new ListBranchesResult { Id = branches[0].Id, Name = "Branch 1" }]);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Branch 1", result[0].Name);
        }
    }
}
