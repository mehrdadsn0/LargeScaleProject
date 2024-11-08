using NotificationProject.Models;

namespace NotificationProject.Services;

public class NotificationService
{
    private readonly NotificationContext _context;

    public NotificationService(NotificationContext context)
    {
        _context = context;
    }

    public (bool, string) SendSMS(string number, string content, int? userId = null)
    {
        try
        {
            SMSMessage message = new SMSMessage();
            message.Number = number;
            message.Content = content;
            message.UserId = userId;
            message.Succes = true;
            _context.SMSMessages.Add(message);
            _context.SaveChanges();
            Console.WriteLine("########");
            Console.WriteLine($"sent content: \n '{content}' \n to: {number} ");
            return (true, $"sent sms to: {number} ");
        }
        catch(Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public (bool, string) SendEmail(string email, string content, int? userId = null)
    {
        try
        {
            EmailMessage message = new EmailMessage();
            message.EmailAddress = email;
            message.Content = content;
            message.UserId = userId;
            message.Succes = true;
            _context.EmailMessages.Add(message);
            _context.SaveChanges();
            Console.WriteLine("########");
            Console.WriteLine($"sent content: \n '{content}' \nto: {email} ");
            return (true, $"sent email to: {email} ");
        }
        catch(Exception ex)
        {
            return (false, ex.Message);
        }
    }
}