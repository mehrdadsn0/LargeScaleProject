using System.ComponentModel.DataAnnotations;

namespace OrdersProject.Models.Dtos;

public record AddOrderDto
{
    [Required]
    public int UserId { get; set; }
    public IEnumerable<AddOrderDetailDto> OrderDetails { get; set; } = null!;

}