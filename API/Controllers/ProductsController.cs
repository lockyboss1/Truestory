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
    public async Task<IActionResult> Get(string? name = null, int page = 1, int pageSize = 10)
    {
        var products = await _service.GetProductsAsync(name, page, pageSize);
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductDto dto)
    {
        try
        {
            ProductValidator.Validate(dto);
            var result = await _service.AddProductAsync(new Product { Name = dto.Name, Data = dto.Data });
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _service.DeleteProductAsync(id);
        return success ? NoContent() : NotFound();
    }
}