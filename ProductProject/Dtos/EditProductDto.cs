namespace ProductProject.Dtos;

public record EditProductDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }
}