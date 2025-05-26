public interface IProductService
{
    Task<IEnumerable<Product>> GetProductsAsync(string nameFilter, int page, int pageSize);
    Task<Product> AddProductAsync(ProductDto dto);
    Task<bool> DeleteProductAsync(string id);
}