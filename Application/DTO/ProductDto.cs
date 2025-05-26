/// <summary>
/// DTO used for creating or updating a product.
/// </summary>
public class ProductDto
{
    /// <summary>
    /// The product's name. This is a required field.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Key-value pairs of product data attributes.
    /// </summary>
    public Dictionary<string, object>? Data { get; set; }
}