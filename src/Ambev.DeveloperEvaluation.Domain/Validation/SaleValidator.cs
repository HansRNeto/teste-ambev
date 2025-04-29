using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validates the Sale entity to ensure data consistency.
/// </summary>
public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(s => s.SaleNumber)
            .NotEmpty()
            .WithMessage("Sale number must be provided.");

        RuleFor(s => s.SaleDate)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Sale date cannot be in the future.");

        RuleFor(s => s.CustomerId)
            .NotEmpty()
            .WithMessage("Customer must be specified.");

        RuleFor(s => s.CustomerName)
            .NotEmpty()
            .WithMessage("Customer name must be provided.");

        RuleFor(s => s.BranchId)
            .NotEmpty()
            .WithMessage("Branch must be specified.");

        RuleFor(s => s.BranchName)
            .NotEmpty()
            .WithMessage("Branch name must be provided.");

        RuleFor(s => s.TotalAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total amount must be greater than or equal to zero.");

        RuleForEach(s => s.SaleItems)
            .SetValidator(new SaleItemValidator());
    }
}