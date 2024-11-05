using System.ComponentModel.DataAnnotations.Schema;

namespace OrdersProject.Models;

public class OrderDetail
{
    public int Id { get; set; }
    [ForeignKey(nameof(Order))]
    public int OrderId { get; set; }
    public virtual Order Order { get; set; } = null!;
    public int ProductId { get; set; }
    public decimal ProductPrice { get; set; }
    public int Count { get; set; }
}