using FluentValidation;

public class ProductDtoValidator : AbstractValidator<ProductDto>
{
    public ProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Data)
            .NotNull().WithMessage("Data cannot be null.")
            .Must(data => data.Any()).WithMessage("Data must contain at least one key-value pair.");
    }
}