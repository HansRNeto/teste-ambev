using Ambev.DeveloperEvaluation.Application.Customer.ListCustomers;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customer.Validators
{
    public class ListCustomersValidatorTests
    {
        private readonly ListCustomersValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_PageNumber_Is_Zero()
        {
            var command = new ListCustomersCommand { PageNumber = 0, PageSize = 10 };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PageNumber)
                .WithErrorMessage("Page number must be greater than zero.");
        }

        [Fact]
        public void Should_Have_Error_When_PageSize_Is_Zero()
        {
            var command = new ListCustomersCommand { PageNumber = 1, PageSize = 0 };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PageSize)
                .WithErrorMessage("Page size must be greater than zero.");
        }

        [Fact]
        public void Should_Have_Error_When_PageSize_Is_Greater_Than_100()
        {
            var command = new ListCustomersCommand { PageNumber = 1, PageSize = 101 };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PageSize)
                .WithErrorMessage("Page size must be less than or equal to 100.");
        }

        [Fact]
        public void Should_Not_Have_Error_Whe_nPageNumber_And_PageSize_Are_Valid()
        {
            var command = new ListCustomersCommand { PageNumber = 1, PageSize = 10 };

            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.PageNumber);
            result.ShouldNotHaveValidationErrorFor(x => x.PageSize);
        }
    }
}