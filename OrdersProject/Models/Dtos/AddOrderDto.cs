namespace OrdersProject.Models.Dtos;

public record AddOrderDto
{
    public int UserId { get; set; }
    public IEnumerable<AddOrderDetailDto> OrderDetails { get; set; } = null!;

}