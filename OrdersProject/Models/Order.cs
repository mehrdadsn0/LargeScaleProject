namespace OrdersProject.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }

    public Order(int userId)
    {
        OrderDetails = new List<OrderDetail>();
    }

}