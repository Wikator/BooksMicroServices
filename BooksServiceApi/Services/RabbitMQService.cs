using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BooksServiceApi.Services;

public class OrderDeletedBackgroundService(IConnection connection, BooksService booksService) : BackgroundService
{
    private const string QueueName = "order-deleted";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var channel = connection.CreateModel();
        
        channel.QueueDeclare(queue: QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Received message: {message}");
            
            var deserializedMessage = JsonSerializer.Deserialize<Message>(message);
            var book = await booksService.GetAsync(deserializedMessage!.BookId);
            await booksService.UpdateStock(book!, -deserializedMessage.Quantity);
        };
        channel.BasicConsume(queue: QueueName,
            autoAck: true,
            consumer: consumer);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}

public class Message
{
    [JsonPropertyName("book_id")]
    public required string BookId { get; init; }
    
    [JsonPropertyName("quantity_to_restore")]
    public int Quantity { get; init; }
}