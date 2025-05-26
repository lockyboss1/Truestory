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

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] ProductQueryDto query)
    {
        var products = await _service.GetProductsAsync(query);
        return Ok(new ApiResponse<IEnumerable<Product>>(products, "Products retrieved successfully"));
    }

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        await _service.DeleteProductAsync(id);
        return Ok(new { success = true, message = $"Product {id} successfully deleted" });
    }
}