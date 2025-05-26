using FluentValidation;

public class ProductDtoValidator : AbstractValidator<ProductDto>
{
    public ProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Data)
            .NotNull().WithMessage("Data is required.")
            .DependentRules(() =>
            {
                RuleFor(x => x.Data)
                    .Must(data => data!.Any())
                    .WithMessage("Data must contain at least one key-value pair.");
            });

        RuleForEach(x => x.Data)
            .Must(kv => kv.Value != null && !string.IsNullOrWhiteSpace(kv.Value.ToString()))
            .WithMessage((productDto, kv) => $"Value for key '{kv.Key}' must not be null or empty.");
    }
}