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
    public async Task<IActionResult> GetProducts(string? name = null, int page = 1, int pageSize = 10)
    {
        try
        {
            var products = await _service.GetProductsAsync(name, page, pageSize);
            return Ok(new ApiResponse<IEnumerable<Product>>(products, "Products retrieved successfully"));
        }
        catch
        {
            return StatusCode(500, new ApiResponse<string>(null, "Failed to retrieve products", false));
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto dto)
    {
        try
        {
            var result = await _service.AddProductAsync(dto);
            var response = new ApiResponse<Product>(result, "Product successfully added");
            return CreatedAtAction(nameof(GetProducts), new { name = result.Name }, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse<string>(null, ex.Message, false));
        }
        catch
        {
            return StatusCode(500, new ApiResponse<string>(null, "An unexpected error occurred.", false));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var success = await _service.DeleteProductAsync(id);

        if (success)
        {
            return Ok(new ApiResponse<string>(null, "Product deleted successfully"));
        }
        else
        {
            return NotFound(new ApiResponse<string>(null, $"Product with ID {id} not found", false));
        }
    }
}