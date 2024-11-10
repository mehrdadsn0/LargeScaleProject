using System.Text;
using System.Text.Json;
using OrdersProject.Models.Dtos;
using RabbitMQ.Client;

namespace OrdersProject.Services;

public class NotificationEventService
{
    public void SendNotification(string content, string? number = null, string? email = null, int? userId = null)
    {
        var factory = new ConnectionFactory() { HostName = "rabbitmq" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "Notification",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            NotificationEventDto message = new(){
                Number = number,
                Content = content,
                Email = email,
                UserId = userId,
            };
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish(exchange: "",
                                 routingKey: "Notification",
                                 basicProperties: null,
                                 body: body);

        }
    }
}