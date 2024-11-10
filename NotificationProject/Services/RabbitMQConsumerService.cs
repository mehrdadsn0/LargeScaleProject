using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using NotificationProject.Models.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NotificationProject.Services;

public class RabbitMQConsumerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private IConnection? _connection;
    private IModel? _channel;
    private const string QueueName = "Notification";

    public RabbitMQConsumerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private void InitializeRabbitMq()
    {
        var factory = new ConnectionFactory
        {
            HostName = "rabbitmq",
            UserName = "guest",
            Password = "guest"
        };

        _connection = factory.CreateConnection();

        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        InitializeRabbitMq();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var jsonMessage = Encoding.UTF8.GetString(body);
            var message = JsonSerializer.Deserialize<NotificationEventDto>(jsonMessage);

            ProcessMessage(message);
        };

        _channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);

        await Task.CompletedTask;
    }

    private async void ProcessMessage(NotificationEventDto? message)
    {
        if (message != null)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                if (message.Email == null && message.Number == null)
                {
                    HttpClient client = new HttpClient()
                    {
                        Timeout = TimeSpan.FromSeconds(60)
                    }; 
                    client.DefaultRequestHeaders.ConnectionClose = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                    var url = $"http://auth:8080/Auth/getusercontact/{message.UserId}";
                    HttpResponseMessage response;
                    try
                    {
                        response = await client.GetAsync(url);
                        string responseBody = await response.Content.ReadAsStringAsync();
                        // var jsonMessage = Encoding.UTF8.GetString(responseBody);
                        var contacts = JsonSerializer.Deserialize<GetUserContactResult>(responseBody);
                        if (contacts is not null)
                        {
                            message.Email = contacts.Email;
                            message.Number = contacts.Number;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                }
                var notificationService = scope.ServiceProvider.GetRequiredService<NotificationService>();
                if (message.Number != null)
                {
                    notificationService.SendSMS(message.Number, message.Content);
                }
                if (message.Email != null)
                {
                    notificationService.SendEmail(message.Email, message.Content);
                }
            }
        }
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        base.Dispose();
    }
}