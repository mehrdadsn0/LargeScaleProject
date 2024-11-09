using Microsoft.AspNetCore.Mvc;
using OrdersProject.Models;
using OrdersProject.Models.Dtos;
using OrdersProject.Services;

namespace OrdersProject.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrdersRepository _repo;
    private readonly NotificationEventService _notificationEventService;

    public OrderController(OrdersRepository repo, NotificationEventService notificationEventService)
    {
        _repo = repo;
        _notificationEventService = notificationEventService;
    }

    [HttpPost]
    public ActionResult<Order> AddOrder(AddOrderDto input)
    {
        Order order = new Order(input.UserId);
        order.Date = DateTime.UtcNow;
        foreach (var item in input.OrderDetails)
        {
            OrderDetail orderDetail = new(item.ProductId, item.ProductPrice, item.Count);
            order.OrderDetails.Add(orderDetail);
            order.TotalPrice += item.ProductPrice * item.Count;
        }

        var (res, message) = _repo.AddOrder(order);
        if (res)
        {
            _notificationEventService.SendNotification(content: $"order number: {order.Id} created", userId: input.UserId);
            return Ok(order);
        }
        else
        {
            return BadRequest(message);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Order> GetOrder(int id)
    {
        Order? order = _repo.GetOrder(id);
        if (order != null)
        {
            return Ok(order);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<Order>> GetOrders()
    {
        return Ok(_repo.GetOrders());
    }
}
