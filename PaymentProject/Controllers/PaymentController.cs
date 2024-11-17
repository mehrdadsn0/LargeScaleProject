using Microsoft.AspNetCore.Mvc;
using PaymentProject.Models;
using PaymentProject.Models.Dtos;

namespace PaymentProject.Controllers;

[ApiController]
[Route("payment")]
public class PaymentController : ControllerBase
{
    private readonly PaymentContext _context;
    public PaymentController(PaymentContext context)
    {
        _context = context;
    }
    [HttpGet(":id")]
    public ActionResult<Payment?> GetPayment(int id)
    {
        Payment? payment = _context.Payments.Find(id);
        if (payment == null)
        {
            return NotFound();
        }
        return Ok(payment);
    }

    [HttpPost]
    public ActionResult<Payment> AddPayment(AddPaymentDto input)
    {
        Payment payment = new Payment();
        payment.OrderId = input.OrderId;
        payment.Amount = input.Amount;

        try
        {
            Random rnd = new Random();
            int luck = rnd.Next(1, 5);
            if (luck != 4)
            {
                payment.Success = true;
            }
            else
            {
                payment.Success = false;
            }
            _context.Payments.Add(payment);
            _context.SaveChanges();
            if (payment.Success)
            {
                return Ok(payment);
            }
            else
            {
                return BadRequest(payment);

            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
