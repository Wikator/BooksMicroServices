using System.Text;
using System.Text.Json;
using BooksServiceApi.Models;
using RabbitMQ.Client;

namespace BooksServiceApi.Publishers;

public abstract class PublisherBase
{
    private readonly IModel _channel;

    private readonly string _queueName;

    protected PublisherBase(IConnection connection, string queueName)
    {
        _channel = connection.CreateModel();
        _channel.QueueDeclare(queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        _queueName = queueName;
    }

    public void Publish(object book)
    {
        var message = book switch
        {
            string s => s,
            Book b => JsonSerializer.Serialize(b),
            _ => throw new ArgumentException("Invalid message type")
        };
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: string.Empty,
            routingKey: _queueName,
            basicProperties: null,
            body: body);
        
        Console.WriteLine($" [x] Sent {message}");
    }
}
