using Refit;

public interface IRestfulApiClient
{
    [Get("/objects")]
    Task<List<Product>> GetProducts();

    [Post("/objects")]
    Task<Product> CreateProduct([Body] Product product);

    [Delete("/objects/{id}")]
    Task DeleteProduct(string id);
}