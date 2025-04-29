using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class CustomerTests
    {
        [Fact]
        public void Validate_Should_Return_Valid_When_Customer_Is_Valid()
        {
            var customer = new Customer
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                IsActive = true
            };

            var validationResult = customer.Validate();

            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Validate_Should_Return_Invalid_When_Customer_Name_Is_Empty()
        {
            var customer = new Customer
            {
                Name = string.Empty, // Invalid name
                Email = "john.doe@example.com",
                IsActive = true
            };

            var validationResult = customer.Validate();

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(error => error.Error == "NotEmptyValidator" && error.Detail.Contains("Customer name cannot be empty."));
        }

        [Fact]
        public void Validate_Should_Return_Invalid_When_Email_Is_Invalid()
        {
            var customer = new Customer
            {
                Name = "John Doe",
                Email = "invalid-email",
                IsActive = true
            };

            var validationResult = customer.Validate();

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(error => error.Error == "EmailValidator" && error.Detail.Contains("Invalid email format."));
        }

        [Fact]
        public void Validate_Should_Return_Invalid_When_Is_Active_Is_Null()
        {
            var customer = new Customer
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                IsActive = null
            };

            var validationResult = customer.Validate();

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(error => error.Error == "NotNullValidator" && error.Detail.Contains("Active status is required."));
        }
    }
}
