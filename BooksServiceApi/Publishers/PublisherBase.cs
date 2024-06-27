using System.Text;
using System.Text.Json;
using BooksServiceApi.Models;
using RabbitMQ.Client;

namespace BooksServiceApi.Publishers;

public abstract class PublisherBase : IDisposable
{
    private const string HostName = "rabbitmq.books-platform";
    private const string UserName = "admin";
    private const string Password = "password";

    private readonly IConnection _connection;
    private readonly IModel _channel;
    private bool _disposed;

    private readonly string _queueName;

    protected PublisherBase(string queueName)
    {
        var factory = new ConnectionFactory { HostName = HostName, UserName = UserName, Password = Password };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        _queueName = queueName;
    }

    public void Publish(object book)
    {
        ObjectDisposedException.ThrowIf(_disposed, nameof(BookChangedPublisher));

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

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
            return;
        
        if (disposing)
        {
            _channel.Close();
            _connection.Close();
        }

        _disposed = true;
    }

    ~PublisherBase()
    {
        Dispose(false);
    }
}
