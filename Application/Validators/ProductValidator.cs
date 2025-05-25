public class ProductValidator
{
    public static void Validate(ProductDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Name is required.");
        if (dto.Data == null)
            throw new ArgumentException("Data is required.");
    }
}