namespace ProductProject.Dtos;

public record AddProductDto
{
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }
}