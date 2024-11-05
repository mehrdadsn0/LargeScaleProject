using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrdersProject.Models;

public class OrderDetail
{
    public int Id { get; set; }
    [ForeignKey(nameof(Order))]
    public int OrderId { get; set; }
    [JsonIgnore]
    public virtual Order Order { get; set; } = null!;
    public int ProductId { get; set; }
    public decimal ProductPrice { get; set; }
    public int Count { get; set; }

    public OrderDetail(int productId, decimal productPrice, int count)
    {
        ProductId = productId;
        ProductPrice = productPrice;
        Count = count;
    }
}