using Microsoft.EntityFrameworkCore;

namespace OrdersProject.Models;

public class OrdersRepository
{
    private readonly OrdersContext _context;

    public OrdersRepository(OrdersContext context)
    {
        _context = context;
    }

    public (bool, string) AddOrder(Order order)
    {
        try
        {
            _context.Orders.Add(order);
            foreach(OrderDetail item in order.OrderDetails){
                item.OrderId = order.Id;
                _context.OrderDetails.Add(item);
            }
            _context.SaveChanges();
            return (true, "success");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public Order? GetOrder(int id)
    {
        return _context.Orders.Include(o => o.OrderDetails).SingleOrDefault(o => o.Id == id);
    }

    public IEnumerable<Order> GetOrders()
    {
        return _context.Orders.Include(o => o.OrderDetails).ToList();
    }
}