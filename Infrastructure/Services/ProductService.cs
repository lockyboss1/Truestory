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

    /// <inheritdoc />
    public async Task<IEnumerable<Product>> GetProductsAsync(ProductQueryDto query)
    {
        var items = await _client.GetProducts();

        var filtered = items
            .Where(p => string.IsNullOrWhiteSpace(query.NameFilter) ||
                        (!string.IsNullOrEmpty(p.Name) &&
                         p.Name.Contains(query.NameFilter, StringComparison.OrdinalIgnoreCase)));

        return filtered
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize);
    }

    /// <inheritdoc />
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
            var response = await _client.CreateProduct(product);
            return response;
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"API error: {ex.StatusCode} - {ex.Message}");
            throw new ApplicationException("API error occurred.");
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeleteProductAsync(string id)
    {
        try
        {
            await _client.DeleteProduct(id);
            return true;
        }
        catch (ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new KeyNotFoundException($"Product with id {id} not found");
        }
        catch (ApiException ex)
        {
            throw new Exception($"Failed to delete product {id}: {ex.Message}");
        }
    }
}