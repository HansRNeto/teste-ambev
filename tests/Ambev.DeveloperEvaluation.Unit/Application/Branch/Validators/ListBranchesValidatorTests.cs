using Ambev.DeveloperEvaluation.Application.Branch.ListBranches;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branch.Validators
{
    public class ListBranchesValidatorTests
    {
        private readonly ListBranchesValidator _validator;

        public ListBranchesValidatorTests()
        {
            _validator = new ListBranchesValidator();
        }

        [Fact]
        public void Should_Have_Error_When_PageNumber_Is_Zero()
        {
            var model = new ListBranchesCommand { PageNumber = 0, PageSize = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.PageNumber);
        }

        [Fact]
        public void Should_Have_Error_When_PageSize_Is_Zero()
        {
            var model = new ListBranchesCommand { PageNumber = 1, PageSize = 0 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.PageSize);
        }

        [Fact]
        public void Should_Have_Error_When_PageSize_Exceeds_100()
        {
            var model = new ListBranchesCommand { PageNumber = 1, PageSize = 101 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.PageSize);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Model_Is_Valid()
        {
            var model = new ListBranchesCommand { PageNumber = 1, PageSize = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}