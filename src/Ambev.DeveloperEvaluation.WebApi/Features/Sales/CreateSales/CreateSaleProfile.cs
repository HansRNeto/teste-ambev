using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using AutoMapper;

/// <summary>
/// AutoMapper profile for mapping between sale-related request, command, result, and response models.
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleProfile"/> class.
    /// Defines the mapping between <see cref="CreateSaleRequest"/> and <see cref="CreateSaleCommand"/>,
    /// and between <see cref="CreateSaleResult"/> and <see cref="CreateSaleResponse"/>.
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<CreateSaleResult, CreateSaleResponse>();
    }
}