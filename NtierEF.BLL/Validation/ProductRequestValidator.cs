using FluentValidation;
using NtierEF.BLL.DTO.Requests;

namespace NtierEF.BLL.Validation
{
    public class ProductRequestValidator : AbstractValidator<ProductRequest>
    {
        public ProductRequestValidator()
        {
            RuleFor(product => product.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

                 RuleFor(product => product.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(product => product.CategoryID)
                .GreaterThan(0).WithMessage("CategoryId must be greater than 0.");
        }
    }
}
