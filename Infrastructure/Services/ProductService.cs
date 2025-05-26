using FluentValidation;
using Refit;

public class ProductService : IProductService
{
    private readonly IRestfulApiClient _client;
    private readonly IValidator<ProductDto> _validator;


    public ProductService(IRestfulApiClient client, IValidator<ProductDto> validator)
    {
        _client = client;
        _validator = validator;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(string nameFilter, int page, int pageSize)
    {
        var items = await _client.GetProducts();

        var filtered = items
            .Where(p => string.IsNullOrWhiteSpace(nameFilter) ||
                        (!string.IsNullOrEmpty(p.Name) &&
                         p.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase)));

        return filtered
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }

    public async Task<Product> AddProductAsync(ProductDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ArgumentException($"Validation failed: {errors}");
        }

        var product = new Product { Name = dto.Name, Data = dto.Data };

        try
        {
            return await _client.CreateProduct(product);
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"API error: {ex.StatusCode} - {ex.Message}");
            throw new ApplicationException("API error occurred.");
        }
    }

    public async Task<bool> DeleteProductAsync(string id)
    {
        try
        {
            await _client.DeleteProduct(id);
            return true;
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"Failed to delete product {id}: {ex.Message}");
            return false;
        }
    }
}