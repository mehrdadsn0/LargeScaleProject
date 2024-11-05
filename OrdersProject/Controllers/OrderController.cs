using Microsoft.AspNetCore.Mvc;
using OrdersProject.Models;
using OrdersProject.Models.Dtos;

namespace OrdersProject.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrdersRepository _repo;

    public OrderController(OrdersRepository repo)
    {
        _repo = repo;
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
            return Ok(order);
        }
        else
        {
            return BadRequest(message);
        }
    }

    [HttpGet(":id")]
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
