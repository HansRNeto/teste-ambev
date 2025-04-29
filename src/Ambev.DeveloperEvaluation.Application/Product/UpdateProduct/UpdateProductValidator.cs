using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Product.UpdateProduct
{
    /// <summary>
    /// Validator class for validating the <see cref="UpdateProductCommand"/>.
    /// </summary>
    /// <remarks>
    /// This validator ensures that all the fields in the <see cref="UpdateProductCommand"/>
    /// are valid before the command is processed. It includes validation for:
    /// - Name: Cannot be empty.
    /// - Email: Cannot be empty and must be a valid email format.
    /// - IsActive: Defaults to true, and no specific validation is needed for this property.
    /// </remarks>
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductCommandValidator"/> class.
        /// </summary>
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .Length(3, 100).WithMessage("Product name must be between 3 and 100 characters.");

            RuleFor(command => command.Description)
                .NotEmpty().WithMessage("Product description is required.")
                .Length(10, 500).WithMessage("Product description must be between 10 and 500 characters.");

            RuleFor(command => command.Price)
                .GreaterThan(0).WithMessage("Product price must be greater than zero.");

            RuleFor(command => command.IsActive)
                .NotNull().WithMessage("Product active status is required.");
        }
    }
}