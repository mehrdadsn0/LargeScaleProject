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
        if (input.UserId == 0)
        {
            return StatusCode(421, "give user id");
        }
        Order order = new(input.UserId);
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

    [HttpPost("payorder")]
    public async Task<ActionResult> PayOrder(int id)
    {
        Order? order = _repo.GetOrder(id);
        if (order == null)
        {
            return NotFound();
        }

        HttpClient client = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(60)
        };
        client.DefaultRequestHeaders.ConnectionClose = false;
        // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        string url = $"http://payment:8080/Payment";
        HttpResponseMessage response;
        try
        {
            response = await client.PostAsJsonAsync(url, new { OrderId = order.Id, Amount = order.TotalPrice });
            if (response.IsSuccessStatusCode)
            {
                _notificationEventService.SendNotification(content: $"order number: {order.Id} has been payed", userId: order.UserId);
                return Ok($"order {order.Id} has been payed successfully");
            }
            else
            {
                _notificationEventService.SendNotification(content: $"order number: {order.Id} payment failed.", userId: order.UserId);
                return BadRequest($"order {order.Id} payment failed");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
