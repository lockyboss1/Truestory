using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }


    /// <summary>
    /// Gets filtered and paged products.
    /// </summary>
    /// <param name="query">Filter and pagination parameters.</param>
    /// <returns>Filtered list of products with success message.</returns>
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] ProductQueryDto query)
    {
        var products = await _service.GetProductsAsync(query);
        return Ok(new ApiResponse<IEnumerable<Product>>(products, "Products retrieved successfully"));
    }

    /// <summary>
    /// Adds a new product.
    /// </summary>
    /// <param name="dto">Product data transfer object.</param>
    /// <returns>Created product with success message.</returns>
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto dto)
    {
        var product = await _service.AddProductAsync(dto);
        return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, new
        {
            success = true,
            message = "Product successfully added",
            data = product
        });
    }

    /// <summary>
    /// Deletes a product by ID.
    /// </summary>
    /// <param name="id">Product ID.</param>
    /// <returns>Success message or not found.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        await _service.DeleteProductAsync(id);
        return Ok(new { success = true, message = $"Product {id} successfully deleted" });
    }
}