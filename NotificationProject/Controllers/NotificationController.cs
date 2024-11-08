using Microsoft.AspNetCore.Mvc;
using NotificationProject.Models;
using NotificationProject.Models.Dtos;

namespace NotificationProject.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{
    private readonly NotificationContext _context;

    public NotificationController(NotificationContext context)
    {
        _context = context;
    }

    [HttpPost("sendsms")]
    public ActionResult SendSMS(SendSMSDto input)
    {
        try
        {
            SMSMessage message = new SMSMessage();
            message.Number = input.Number;
            message.Content = input.Content;
            message.Succes = true;
            _context.SMSMessages.Add(message);
            _context.SaveChanges();
            Console.WriteLine($"sent sms to: {input.Number} ");
            return StatusCode(201);
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpPost("sendemail")]
    public ActionResult SendEmail(SendEmailDto input)
    {
        try
        {
            EmailMessage message = new EmailMessage();
            message.EmailAddress = input.Email;
            message.Content = input.Content;
            message.Succes = true;
            _context.EmailMessages.Add(message);
            _context.SaveChanges();
            Console.WriteLine($"sent email to: {input.Email} ");
            return StatusCode(201);
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
