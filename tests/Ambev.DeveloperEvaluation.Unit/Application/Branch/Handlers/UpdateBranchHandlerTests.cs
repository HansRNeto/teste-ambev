using Ambev.DeveloperEvaluation.Application.Branch.UpdateBranch;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branch.Handlers;

public class UpdateBranchCommandTests
{
    [Fact]
    public void Validate_ValidCommand_ShouldReturnValidResult()
    {
        var command = new UpdateBranchCommand
        {
            Id = Guid.NewGuid(),
            Name = "Updated Branch Name",
            Address = "123 Main St",
            IsActive = true,
            UpdatedAt = DateTime.Now
        };

        var result = command.Validate();

        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Validate_InvalidCommand_ShouldReturnInvalidResult()
    {
        var command = new UpdateBranchCommand
        {
            Id = Guid.Empty,
            Name = "",
            Address = "",
            IsActive = null,
            UpdatedAt = DateTime.Now
        };

        var result = command.Validate();

        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(e =>
            e.Error == "NotEmptyValidator" && e.Detail.Contains("Branch Id must be provided."));
        result.Errors.Should().Contain(e =>
            e.Error == "NotEmptyValidator" && e.Detail.Contains("Branch name must be provided."));
        result.Errors.Should().Contain(e =>
            e.Error == "NotEmptyValidator" && e.Detail.Contains("Branch address must be provided."));
        result.Errors.Should().Contain(e =>
            e.Error == "NotNullValidator" && e.Detail.Contains("Branch active status must be specified."));
    }
}