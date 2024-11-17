using Microsoft.AspNetCore.Mvc;
using NotificationProject.Models;
using NotificationProject.Models.Dtos;
using NotificationProject.Services;

namespace NotificationProject.Controllers;

[ApiController]
[Route("notification")]
public class NotificationController : ControllerBase
{
    private readonly NotificationService _service;

    public NotificationController(NotificationService service)
    {
        _service = service;
    }

    [HttpPost("sendsms")]
    public ActionResult SendSMS(SendSMSDto input)
    {
        var (res, message) = _service.SendSMS(input.Number, input.Content);
        if (res)
        {
            return StatusCode(201);
        }
        else
        {
            return StatusCode(500, message);
        }
    }
    [HttpPost("sendemail")]
    public ActionResult SendEmail(SendEmailDto input)
    {
        var (res, message) = _service.SendEmail(input.Email, input.Content);
        if (res)
        {
            return StatusCode(201);
        }
        else
        {
            return StatusCode(500, message);
        }
    }
}
