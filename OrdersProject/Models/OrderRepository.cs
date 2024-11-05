namespace OrdersProject.Models;

public class OrdersRepository
{
    private readonly OrdersContext _context;

    public OrdersRepository(OrdersContext context)
    {
        _context = context;
    }

    public (bool, Order) AddOrder(Order order)
    {
        try
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return (true, order);
        }
        catch
        {
            return (false, order);
        }
    }

    public Order? GetOrder(int id)
    {
        return _context.Orders.Find(id);
    }

    public IEnumerable<Order> GetOrders()
    {
        return _context.Orders;
    }
}