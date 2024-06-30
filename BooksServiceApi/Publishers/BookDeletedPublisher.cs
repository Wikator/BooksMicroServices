using RabbitMQ.Client;

namespace BooksServiceApi.Publishers;

public class BookDeletedPublisher(IConnection connection) : PublisherBase(connection, "book-deleted-exchange");