using Refit;

/// <summary>
/// Refit interface for interacting with the external RESTful API at https://api.restful-api.dev.
/// </summary>
public interface IRestfulApiClient
{
    /// <summary>
    /// Retrieves the list of products from the external API.
    /// </summary>
    [Get("/objects")]
    Task<List<Product>> GetProducts();

    /// <summary>
    /// Creates a new product by posting to the external API.
    /// </summary>
    /// <param name="product">The product to create.</param>
    [Post("/objects")]
    Task<Product> CreateProduct([Body] Product product);

    /// <summary>
    /// Deletes a product by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the product to delete.</param>
    [Delete("/objects/{id}")]
    Task DeleteProduct(string id);
}