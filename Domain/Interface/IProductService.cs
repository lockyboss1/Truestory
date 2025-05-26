/// <summary>
/// Service interface for product operations.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Retrieves products filtered by name and paged.
    /// </summary>
    /// <param name="query">Filter and pagination parameters.</param>
    /// <returns>Enumerable list of matching products.</returns>
    Task<IEnumerable<Product>> GetProductsAsync(ProductQueryDto query);

    /// <summary>
    /// Adds a new product after validation.
    /// </summary>
    /// <param name="product">Product to add.</param>
    /// <returns>The added product with assigned ID.</returns>
    /// <exception cref="ArgumentException">Thrown if validation fails.</exception>
    Task<Product> AddProductAsync(ProductDto dto);

    /// <summary>
    /// Deletes a product by ID.
    /// </summary>
    /// <param name="id">ID of the product to delete.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if product does not exist.</exception>
    /// <exception cref="Exception">Thrown if deletion fails.</exception>
    Task<bool> DeleteProductAsync(string id);
}