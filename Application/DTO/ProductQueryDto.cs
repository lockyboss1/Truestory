/// <summary>
/// Data Transfer Object used to query products with filtering and paging.
/// </summary>
public class ProductQueryDto
{
    /// <summary>
    /// Optional substring filter for product names.
    /// </summary>
    public string? NameFilter { get; set; }

    /// <summary>
    /// Page number for pagination (1-based).
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Number of items per page.
    /// </summary>
    public int PageSize { get; set; } = 10;
}