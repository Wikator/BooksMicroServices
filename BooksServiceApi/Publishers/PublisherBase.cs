using System.Text;
using System.Text.Json;
using BooksServiceApi.Models;
using RabbitMQ.Client;

namespace BooksServiceApi.Publishers;

public abstract class PublisherBase
{
    private readonly IModel _channel;

    private readonly string _exchangeName;

    protected PublisherBase(IConnection connection, string exchangeName)
    {
        _channel = connection.CreateModel();
        _channel.ExchangeDeclare(exchange: exchangeName,
            type: "direct",
            durable: true,
            autoDelete: false,
            arguments: null);

        _exchangeName = exchangeName;
    }

    public void Publish(object book, params string[] routingKeys)
    {
        var message = book switch
        {
            string s => s,
            Book b => JsonSerializer.Serialize(b),
            _ => throw new ArgumentException("Invalid message type")
        };
        var body = Encoding.UTF8.GetBytes(message);

        foreach (var routingKey in routingKeys)
        {
            _channel.BasicPublish(exchange: _exchangeName,
                routingKey: routingKey,
                basicProperties: null,
                body: body);
        }
        
        Console.WriteLine($" [x] Sent {message}");
    }
}
