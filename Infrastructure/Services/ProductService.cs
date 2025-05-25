public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(string nameFilter, int page, int pageSize)
    {
        var response = await _httpClient.GetFromJsonAsync<List<Product>>("https://api.restful-api.dev/objects");
        var filtered = response
            .Where(p => string.IsNullOrWhiteSpace(nameFilter) || p.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase))
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
        return filtered;
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        var response = await _httpClient.PostAsJsonAsync("https://restful-api.dev/api/products", product);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Product>();
    }

    public async Task<bool> DeleteProductAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"https://restful-api.dev/api/products/{id}");
        return response.IsSuccessStatusCode;
    }
}