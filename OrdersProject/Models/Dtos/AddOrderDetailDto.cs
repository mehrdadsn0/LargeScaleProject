namespace OrdersProject.Models.Dtos;

public record AddOrderDetailDto
{
    public int ProductId { get; set; }
    public decimal ProductPrice { get; set; }
    public int Count { get; set; }
}