using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class BranchTests
    {
        [Fact]
        public void Validate_Should_Return_Valid_When_Branch_Is_Valid()
        {
            var branch = new Branch
            {
                Name = "Main Branch",
                Address = "123 Main Street",
                IsActive = true
            };

            var validationResult = branch.Validate();

            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Validate_Should_Return_Invalid_When_Address_Is_Empty()
        {
            var branch = new Branch
            {
                Name = "Main Branch",
                Address = string.Empty,
                IsActive = true
            };

            var validationResult = branch.Validate();

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(error =>
                error.Error == "NotEmptyValidator" && error.Detail.Contains("Address cannot be empty"));
        }

        [Fact]
        public void Validate_Should_Return_Invalid_When_IsActive_Is_Null()
        {
            var branch = new Branch
            {
                Name = "Main Branch",
                Address = "123 Main Street",
                IsActive = null
            };

            var validationResult = branch.Validate();

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(error =>
                error.Error == "NotNullValidator" && error.Detail.Contains("Active status is required"));
        }
    }
}