public interface IProductService
{
    Task<IEnumerable<Product>> GetProductsAsync(ProductQueryDto query);
    Task<Product> AddProductAsync(ProductDto dto);
    Task<bool> DeleteProductAsync(string id);
}