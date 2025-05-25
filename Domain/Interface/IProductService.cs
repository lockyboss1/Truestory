public interface IProductService
{
    Task<IEnumerable<Product>> GetProductsAsync(string nameFilter, int page, int pageSize);
    Task<Product> AddProductAsync(Product product);
    Task<bool> DeleteProductAsync(string id);
}